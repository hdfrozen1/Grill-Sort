using UnityEngine;

[CreateAssetMenu(fileName = "LevelData", menuName = "Scriptable Objects/LevelData")]
public class LevelData : ScriptableObject
{
    public int Level;
    public int TimeToBeat;
    public int TotalFood;
    public int TotalGrill;
}
