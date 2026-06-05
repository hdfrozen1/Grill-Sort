using System.Collections;
using UnityEngine;
using TMPro;
using System;

public class TimeManager : MonoBehaviour
{
    private static TimeManager _instance;
    public static TimeManager Instance => _instance;
    [SerializeField] private TextMeshProUGUI buttonTextLevel;
    [SerializeField] private TextMeshProUGUI ingameTextLevel;
    [SerializeField] private TextMeshProUGUI ingameTextTime;

    public int Time; 
    private bool isPause = false; // Biến bool quản lý trạng thái tạm dừng
    public event Action<bool> OnOutOffTime;
    private Coroutine timerCoroutine;
    private void Awake() {
        _instance = this;
    }
    public void SetLevelDisplay(int currentLevel)
    {
        if (buttonTextLevel != null) buttonTextLevel.text = "Level " + currentLevel;
        if (ingameTextLevel != null) ingameTextLevel.text = "Level " + currentLevel;
    }

    public void StartTimer()
    {
        isPause = false; // Đảm bảo bắt đầu game là chạy luôn, không bị giữ trạng thái pause cũ
        if (timerCoroutine == null)
        {
            timerCoroutine = StartCoroutine(TimerRoutine());
        }
        
    }

    // Thay toàn bộ nội dung hàm pause cũ thành đảo trạng thái isPause
    public void PauseTimer()
    {
        isPause = !isPause;
        Debug.Log(isPause ? "Đã tạm dừng game!" : "Đã tiếp tục chơi!");
    }
    public void StopTime()
    {
        StopCoroutine(timerCoroutine);
    }

    private IEnumerator TimerRoutine()
    {
        while (Time > 0)
        {
            // Thêm điều kiện !isPause theo yêu cầu của bạn
            if (!isPause)
            {
                UpdateTimerUI();
                yield return new WaitForSeconds(1f);
                Time--;
            }
            else
            {
                // BẮT BUỘC PHẢI CÓ: Đợi 1 frame rồi check lại, tránh loop vô hạn làm crash Unity
                yield return null; 
            }
        }

        UpdateTimerUI();
        Debug.Log("Hết giờ!");
        StopTime();
        timerCoroutine = null;
        OnOutOffTime?.Invoke(false);
    }

    private void UpdateTimerUI()
    {
        if (ingameTextTime != null)
        {
            int minutes = Time / 60;
            int seconds = Time % 60;
            ingameTextTime.text = string.Format("{0:00}:{1:00}", minutes, seconds);
        }
    }
}