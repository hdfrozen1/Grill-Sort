using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Game Event", menuName = "SO/Game Event")]
public class GameEvent : ScriptableObject
{
    //private List<System.Action> listeners = new List<System.Action>();
    public string EventName;
    
    public void Raise()
    {
        ManagerUIEventObject.Instance.ActiveEventObject(EventName);
    }
}