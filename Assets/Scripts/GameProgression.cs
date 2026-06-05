using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public struct LevelMilestone
{
    public int unlockAtLevel; // Màn hình cụ thể để mở tính năng
    public GameEvent unlockEvent; // ScriptableObject Event của bạn
}
[System.Serializable]
public class PlayerSaveData
{
    public List<bool> UIEnventState;
    public int CurrentHealth;
    public int CurrentGold;
    public int CurrentLevel;
    public int NumberOfMagnet;
    public int NumberOfSwap;
    public int NumberOfAddTray;
}

[CreateAssetMenu(fileName = "GameProgression", menuName = "Configs/Progression")]
public class GameProgression : ScriptableObject
{
    public List<bool> UIEnventState; // trạng thái của các ui event object đã active hay chưa
    public int CurrentHealth;
    public int CurrentGold;
    [Header("Level Information")]
    public List<LevelData> AllLevels;
    public int CurrentLevel;

    [Header("Boosts Item Information")]
    public int NumberOfMagnet;
    public int NumberOfSwap;
    public int NumberOfAddTray;
    public List<LevelMilestone> milestones;

    private const string SaveKey = "UserGameData";
    public void Save()
    {
        UIEnventState.Clear();
        foreach(GameObject theObject in ManagerUIEventObject.Instance.EventObjects)
        {
            UIEnventState.Add(theObject.activeSelf);
        }
        #if UNITY_EDITOR 
        UnityEditor.EditorUtility.SetDirty(this);
        Debug.Log("Đã SetDirty trong Editor!");

        #elif UNITY_ANDROID|| UNITY_WEBGL
        // 2. Khi lưu: Tạo một bản pack data trung gian, chỉ gán các biến cần thiết
        PlayerSaveData saveData = new PlayerSaveData();
        saveData.UIEnventState = this.UIEnventState;
        saveData.CurrentHealth = this.CurrentHealth;
        saveData.CurrentGold = this.CurrentGold;
        saveData.CurrentLevel = this.CurrentLevel;
        saveData.NumberOfMagnet = this.NumberOfMagnet;
        saveData.NumberOfSwap = this.NumberOfSwap;
        saveData.NumberOfAddTray = this.NumberOfAddTray;

        // Tiến hành lưu JSON của class trung gian này
        string json = JsonUtility.ToJson(saveData);
        PlayerPrefs.SetString(SaveKey, json);
        PlayerPrefs.Save();
        Debug.Log("Đã lưu các chỉ số (Bỏ qua AllLevels) trên Android!");
        #endif
    }
    public void Load()
    {
        #if UNITY_ANDROID || UNITY_WEBGL
        if (PlayerPrefs.HasKey(SaveKey))
        {
            string json = PlayerPrefs.GetString(SaveKey);
            
            // 3. Khi load: Đọc ra Class trung gian rồi gán ngược lại cho các biến runtime
            PlayerSaveData saveData = JsonUtility.FromJson<PlayerSaveData>(json);
            
            this.UIEnventState = saveData.UIEnventState;
            this.CurrentHealth = saveData.CurrentHealth;
            this.CurrentGold = saveData.CurrentGold;
            this.CurrentLevel = saveData.CurrentLevel;
            this.NumberOfMagnet = saveData.NumberOfMagnet;
            this.NumberOfSwap = saveData.NumberOfSwap;
            this.NumberOfAddTray = saveData.NumberOfAddTray;

            Debug.Log("Đã load dữ liệu thành công! List AllLevels giữ nguyên bản gốc.");
        }
        #endif
    }
    public void CheckAndTrigger()
    {
        
        // Duyệt qua danh sách ít ỏi (dưới 10 cái nên cực nhanh)
        foreach (var milestone in milestones)
        {
            if (milestone.unlockAtLevel == CurrentLevel)
            {
                milestone.unlockEvent.Raise();
            }
        }
    }
    public void OpenActivatedUIEvent()
    {
        ManagerUIEventObject.Instance.LoadUIEventObjectState(UIEnventState);
    }
    public void InitLevel()
    {
        LevelData levelData = AllLevels[(CurrentLevel - 1) % AllLevels.Count];
        GameManagers.Instance.InitLevel(levelData);
        TimeManager.Instance.Time = levelData.TimeToBeat;
    }

}