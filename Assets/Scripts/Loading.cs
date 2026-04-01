using System.Collections;
using DG.Tweening;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Loading : MonoBehaviour
{
    public CanvasGroup _pnLoading;
    void Start()
    {
        DontDestroyOnLoad(this.gameObject);

        
    }

    IEnumerator LoadAsyncScene()
    {
        _pnLoading.gameObject.SetActive(true);
        _pnLoading.alpha= 0;
        _pnLoading.DOFade(1,0.5f);
        yield return new WaitForSeconds(0.5f);
        AsyncOperation asyncload = SceneManager.LoadSceneAsync("Main");
        asyncload.allowSceneActivation = false;
        while(!asyncload.isDone)
        {
            if(asyncload.progress >= 0.9f)
            {
                asyncload.allowSceneActivation = true;
            }
            yield return null;
        }
        _pnLoading.DOFade(0, 1).OnComplete(() =>
        {
            _pnLoading.gameObject.SetActive(false);
        });
    }
}
