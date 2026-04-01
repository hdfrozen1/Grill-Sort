using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using GoogleMobileAds.Api;

public class GoogleAds : MonoBehaviour
{
    // ID Admob (Lưu ý: AppID giờ điền trong Assets > Google Mobile Ads > Settings)
    string BannerID = "ca-app-pub-3940256099942544/6300978111";
    string InterID = "ca-app-pub-3940256099942544/1033173712";
    string VideoID = "ca-app-pub-3940256099942544/5224354917";

    [Header("Ad Units")]
    private InterstitialAd interstitialAd;
    private List<RewardedAd> rewardAdsList = new List<RewardedAd>();
    [SerializeField] int MaxRewardCount = 3;

    public UnityEvent RewardCallBack = new UnityEvent();
    public UnityEvent InterCallBack = new UnityEvent();

    private BannerView bannerView;

    private bool _isInterShowed = false;
    private float timeInterCounter = 0;
    [SerializeField] float MaxTimerInter = 60;
    private bool _Banner_active;

    public float DelayReloadBanner;
    private float DelayReloadInter = 1;
    private int DelayReloadReward = 1;

    void Start()
    {
        // Khởi tạo Mobile Ads SDK
        MobileAds.Initialize(initStatus =>
        {
            // SDK bản mới yêu cầu Load quảng cáo sau khi Init
            // Bạn có thể gọi trực tiếp ở đây
            RequestInterstitial();
            RequestBanner();
            for (int i = 0; i < MaxRewardCount; i++)
            {
                RequestReward();
            }
        });
    }

    void Update()
    {
        if (_isInterShowed)
        {
            timeInterCounter += Time.deltaTime;
            if (timeInterCounter >= MaxTimerInter)
            {
                timeInterCounter = 0;
                _isInterShowed = false;
            }
        }
    }

    #region Banner
    public void RequestBanner()
    {
        if (bannerView != null) bannerView.Destroy();

        // Sử dụng AdSize.Banner (320x50) hoặc AdSize.GetCurrentOrientationAnchoredAdaptiveBannerAdSizeWithWidth
        bannerView = new BannerView(BannerID, AdSize.Banner, AdPosition.Top);
        bannerView.OnBannerAdLoaded += () =>
    {
        Debug.Log("Load banner success!");
        _Banner_active = true;
        // Reset delay khi load thành công
        DelayReloadBanner = 5f;
    };

        bannerView.OnBannerAdLoadFailed += (LoadAdError error) =>
        {
            // error.GetMessage() sẽ cho bạn biết chính xác lý do lỗi
            Debug.LogError("Load banner failed: " + error.GetMessage());
            Invoke("RequestBanner", DelayReloadBanner);
            DelayReloadBanner *= 2; // Thường nhân 2 là đủ, nhân 5 sẽ khiến chờ rất lâu
        };
        AdRequest request = new AdRequest();
        bannerView.LoadAd(request);
    }
    #endregion

    #region Interstitial
    public void RequestInterstitial()
    {
        if (interstitialAd != null)
        {
            interstitialAd.Destroy();
            interstitialAd = null;
        }

        AdRequest request = new AdRequest();

        // Bản 10.x sử dụng Static Load method
        InterstitialAd.Load(InterID, request, (InterstitialAd ad, LoadAdError error) =>
        {
            if (error != null || ad == null)
            {
                Debug.LogError("Inter Load Fail: " + error.GetMessage());
                Invoke("RequestInterstitial", DelayReloadInter);
                DelayReloadInter *= 2;
                return;
            }

            interstitialAd = ad;
            DelayReloadInter = 2;

            // Đăng ký các sự kiện (thay thế cho OnAdClosed cũ)
            interstitialAd.OnAdFullScreenContentClosed += () =>
            {
                Debug.Log("Inter closed.");
                RequestInterstitial(); // Load lại ad mới
                InterCallBack?.Invoke();
                InterCallBack.RemoveAllListeners();
            };
        });
        StartCoroutine(WaitLoadInter());
    }
    IEnumerator WaitLoadInter() {
        yield return new WaitUntil(() => this.interstitialAd.CanShowAd());
        Debug.Log("Inter load done!");
    }
    public void ShowInter()
    {
        if (interstitialAd != null && interstitialAd.CanShowAd())
        {
            if (_isInterShowed) return;

            _isInterShowed = true;
            timeInterCounter = 0;
            interstitialAd.Show();
        }
        else
        {
            Debug.Log("Interstitial not ready.");
        }
    }
    #endregion

    #region Reward
    public void RequestReward()
    {
        AdRequest request = new AdRequest();

        RewardedAd.Load(VideoID, request, (RewardedAd ad, LoadAdError error) =>
        {
            if (error != null || ad == null)
            {
                Debug.LogError("Reward Load Fail: " + error.GetMessage());
                Invoke("RequestReward", DelayReloadReward);
                DelayReloadReward *= 2;
                return;
            }

            rewardAdsList.Add(ad);

            ad.OnAdFullScreenContentClosed += () =>
            {
                rewardAdsList.Remove(ad);
                RequestReward();
            };
        });
    }

    public void ShowReward()
    {
#if UNITY_EDITOR
        RewardCallBack?.Invoke();
        RewardCallBack.RemoveAllListeners();
        return;
#endif
        // Tìm quảng cáo nào đã load xong trong list
        RewardedAd readyAd = rewardAdsList.Find(a => a.CanShowAd());

        if (readyAd != null)
        {
            readyAd.Show((Reward reward) =>
            {
                // Hàm callback khi người dùng nhận thưởng thành công
                Debug.Log("User rewarded!");
                // Thực thi trên main thread
                RewardCallBack?.Invoke();
                RewardCallBack.RemoveAllListeners();
            });
        }
        else
        {
            Debug.LogWarning("Reward Ads not available!");
            if (rewardAdsList.Count < MaxRewardCount) RequestReward();
        }
    }
    #endregion
}