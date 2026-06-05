using UnityEngine;
using UnityEngine.UI;

public class GroupButtonHandler : MonoBehaviour
{
    [SerializeField] private HandleUIInGame groupUI;
    void Start()
    {
        SetUpButton();
    }
    private void SetUpButton()
    {
        Button[] buttons = GetComponentsInChildren<Button>(true);
        for(int i = 0; i < buttons.Length; i++)
        {
            int j = i;
            buttons[i].onClick.AddListener(()=> groupUI.OpenUI(j));
        }
    }
}
