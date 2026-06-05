using System.Collections.Generic;
using UnityEngine;

public class HandleUIInGame : MonoBehaviour
{
    [SerializeField]private List<CanvasGroup> canvasGroups;
    int currentActiveCanvasGroup = -1;
    void Start()
    {
        canvasGroups = new List<CanvasGroup>(GetComponentsInChildren<CanvasGroup>(true));
    }
    public void CloseUI(bool active = true)
    {
        canvasGroups[currentActiveCanvasGroup].alpha = 0; // them dotween vao sau
        canvasGroups[currentActiveCanvasGroup].blocksRaycasts = false;
        canvasGroups[currentActiveCanvasGroup].interactable = false;
        if(!active)
        {
            canvasGroups[currentActiveCanvasGroup].gameObject.SetActive(false);
        }
        currentActiveCanvasGroup = -1;

    }
    public void OpenUI(int index)
    {
        if(canvasGroups[index].gameObject.activeSelf == false)
        {
            canvasGroups[index].gameObject.SetActive(true);
        }
        canvasGroups[index].alpha = 1; // them dotween vao sau
        canvasGroups[index].blocksRaycasts = true;
        canvasGroups[index].interactable = true;
        currentActiveCanvasGroup = index;
    }
}
