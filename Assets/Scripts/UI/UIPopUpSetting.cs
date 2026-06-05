using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIPopUpSetting : MonoBehaviour
{
    //[SerializeField] private List<Button> buttons;
    void Start()
    {
        SetupButtons();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    void SetupButtons()
    {
        Button[] buttons = GetComponentsInChildren<Button>(true);
        buttons[0].onClick.AddListener(() => { Debug.Log("Button 1 clicked"); });
        buttons[1].onClick.AddListener(() => { Debug.Log("Button 2 clicked"); });
        buttons[2].onClick.AddListener(() => { Debug.Log("Button 3 clicked"); });
        buttons[3].onClick.AddListener(() => { Debug.Log("Button 4 clicked"); });
    }
}
