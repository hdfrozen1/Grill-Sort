using System.Collections.Generic;
using UnityEngine;

public class ManagerUIEventObject : MonoBehaviour
{
    public List<GameObject> EventObjects; //các ui event object như : ui hướng dẫn, ui thông tin boost...
    private static ManagerUIEventObject _instance;
    public static ManagerUIEventObject Instance => _instance;
    void Awake()
    {
        _instance = this;
    }
    public void ActiveEventObject(string objectName)
    {
        for(int i = 0; i < EventObjects.Count; i++)
        {
            if(EventObjects[i].name == objectName)
            {
                EventObjects[i].SetActive(EventObjects[i].activeSelf == false);
            }
        }
    }
    public void LoadUIEventObjectState(List<bool> states)
    {
        for(int i = 0; i < states.Count; i++)
        {
            EventObjects[i].SetActive(states[i]);
        }
    }
}
