using System;
using UnityEngine;

public class TestAds : MonoBehaviour
{
    public GoogleAds googleAds;
    void Start()
    {
        googleAds = GetComponent<GoogleAds>();
        googleAds.RewardCallBack.AddListener(OnRewarded);
        googleAds.InterCallBack.AddListener(OnInter);
    }

    private void OnInter()
    {
        Debug.Log("On Inter called");
    }

    private void OnRewarded()
    {
        Debug.Log("Reward received");
    }
    

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.I))
        {
            googleAds.ShowInter();
        }
        if (Input.GetKeyDown(KeyCode.R))
        {
            googleAds.ShowReward();
        }
        
    }
}
