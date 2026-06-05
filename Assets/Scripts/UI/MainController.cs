using System.Collections;
using DG.Tweening;
using UnityEngine;

public class MainController : MonoBehaviour
{
    public GameObject GameCanvas;
    public GameObject HomeCanvas;
    public GameObject LoadingCanvas;
    public GameProgression GameProgression;
    public HandleUIInGame CanvasIngame;
    void Start()
    {
        GameProgression.Load();
        //ManagerUIEventObject.Instance.LoadUIEventObjectState(GameProgression.UIEnventState);
        GameManagers.Instance.OnGameFinish += ShowUIFinish;
        TimeManager.Instance.OnOutOffTime += ShowUIFinish;
        GameProgression.OpenActivatedUIEvent();
        GameProgression.CheckAndTrigger();
        OpenHomeCanvas();
        TimeManager.Instance.SetLevelDisplay(GameProgression.CurrentLevel);
    }
    private void ShowUIFinish(bool win)
    {
        Debug.Log("win : " + win);
        OpenHomeCanvas();
        if (win)
        {
            CanvasIngame.OpenUI(1);//show ui win
        }
        else
        {
            CanvasIngame.OpenUI(0);//show ui lose
        }
    }
    public void OpenHomeCanvas()
    {
        GameCanvas.SetActive(false);
        
        HomeCanvas.SetActive(true);
        LoadingCanvas.SetActive(false);
    }
    public void PlayGame()
    {
        StartCoroutine(PlayGameSequence());

    }
    private IEnumerator PlayGameSequence()
    {
        // 1. Hiển thị màn hình chờ và ẩn menu chính
        HomeCanvas.SetActive(false);
        if (LoadingCanvas != null) LoadingCanvas.SetActive(true);
        CanvasGroup loadingGroup = LoadingCanvas.transform.GetComponentInChildren<CanvasGroup>();
        if(loadingGroup != null)
        {
            loadingGroup.alpha = 1;
        }
        GameCanvas.SetActive(true);

        // 2. Khởi tạo Level
        //GameManagers.Instance.OnInitLevel();
        GameProgression.InitLevel();

        // 3. Vòng lặp chờ cho đến khi GameManagers không còn bận (IsBusy == false)
        while (GameManagers.Instance.IsBusy)
        {
            yield return null; // Đợi đến frame tiếp theo rồi kiểm tra lại
        }

        // 4. Sau khi IsBusy đã false, đợi thêm 2 giây
        yield return new WaitForSeconds(2f);

        // 5. Ẩn màn hình chờ và hiện các Canvas chơi game
        if (LoadingCanvas != null) LoadingCanvas.SetActive(false);
        
        // 6. Sau khi ẩn Loading sẽ hiện lên game, đợi 2 giây rồi bắt đầu tính giờ
        yield return new WaitForSeconds(2f);
        TimeManager.Instance.StartTimer();
    }
    public void GoToNextLevel()
    {
        GameProgression.CurrentLevel += 1;
        TimeManager.Instance.SetLevelDisplay(GameProgression.CurrentLevel);
        GameProgression.CheckAndTrigger();
    }
}
