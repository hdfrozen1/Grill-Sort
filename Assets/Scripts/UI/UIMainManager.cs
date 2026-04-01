using System.Collections.Generic;
using System.Linq;
using DG.Tweening;
using UnityEngine;
using UnityEngine.UI; // Đừng quên cái này nhé!

public class UIMainManager : MonoBehaviour
{
    public Transform NavigationButtonsParent;
    public List<Button> navButtons;
    public List<CanvasGroup> canvasGroups;
    private int currentActiveCanvasGroup;
    [SerializeField]private float fadeDuration;

    void Start()
    {
        navButtons = Utils.GetListInChild<Button>(NavigationButtonsParent);
        //SetupButtons();
        GetAllCanvasGroups();
        SetupNavButtons();
    }
    private void GetAllCanvasGroups()
    {
        canvasGroups = GetComponentsInChildren<CanvasGroup>().ToList();
        foreach(CanvasGroup canvas in canvasGroups)
        {
            canvas.alpha = 0;
            canvas.blocksRaycasts = false;
            canvas.interactable = false;
        }
        canvasGroups[1].alpha = 1;
        canvasGroups[1].blocksRaycasts = true;
        canvasGroups[1].interactable = true;
        currentActiveCanvasGroup = 0;
    }
    private void ShowCanvasGroup(int index)
    {
        CanvasGroup oldGroup = canvasGroups[currentActiveCanvasGroup];
        oldGroup.interactable = false;      // Tắt tương tác ngay lập tức
        oldGroup.blocksRaycasts = false;    // Tắt chặn tia raycast ngay lập tức
        oldGroup.DOFade(0, fadeDuration);   // Giảm Alpha dần về 0

        // 3. Xử lý Canvas mới (Sẽ hiển thị)
        CanvasGroup newGroup = canvasGroups[index];
        newGroup.DOFade(1, fadeDuration);   // Tăng Alpha dần lên 1
        
        // Bật lại tương tác và raycast
        newGroup.interactable = true;
        newGroup.blocksRaycasts = true;
        newGroup.transform.SetAsLastSibling();

        // 4. Cập nhật lại index hiện tại
        currentActiveCanvasGroup = index;

    }
    private void SetupNavButtons()
    {
        for(int i = 0; i < navButtons.Count; i++)
        {
            int j = i;
            navButtons[i].onClick.AddListener(() => ShowCanvasGroup(j));
        }
    }
    public void SetupButtons()
    {
        // 1. Tìm tất cả Button có trong các object con (kể cả đang bị ẩn)
        Button[] allButtons = GetComponentsInChildren<Button>(true);

        foreach (Button btn in allButtons)
        {
            // 2. Xóa các sự kiện cũ để tránh bị trùng lặp nếu gọi hàm này nhiều lần
            btn.onClick.RemoveAllListeners();

            // 3. Gán một hành động chung: Khi click, gọi hàm HandleButtonClick và gửi chính cái nút đó qua
            btn.onClick.AddListener(() => HandleButtonClick(btn.gameObject));
        }
        
        Debug.Log($"<color=green>Đã kết nối thành công {allButtons.Length} nút!</color>");
    }

    private void HandleButtonClick(GameObject clickedObject)
    {
        // 4. Kiểm tra tên của Object để biết nút nào vừa bấm
        string btnName = clickedObject.name;

        switch (btnName)
        {
            case "BtnPlay":
                Debug.Log("Bắt đầu game thôi!");
                break;
            case "BtnSettings":
                Debug.Log("Mở bảng cài đặt");
                break;
            case "BtnQuit":
                Application.Quit();
                break;
            default:
                Debug.Log($"Bạn vừa bấm vào nút: {btnName} nhưng chưa code xử lý.");
                break;
        }
    }
}
