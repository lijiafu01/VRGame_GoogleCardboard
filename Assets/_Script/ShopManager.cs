using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ShopManager : MonoBehaviour
{
    public Image[] buttonPressProcess;  // Mảng các Image hiển thị quá trình nhấn nút
    public static ShopManager Instance { get; private set; }  // Singleton instance
    public Button[] buttons;  // Mảng các nút trong cửa hàng

    private Coroutine currentProcess;  // Lưu trữ coroutine hiện tại để có thể hủy nếu cần

    public void UpdateText(int buttonIndex)
    {
        if (buttonIndex >= 0 && buttonIndex < buttons.Length)
        {
            TextMeshProUGUI buttonText = buttons[buttonIndex].GetComponentInChildren<TextMeshProUGUI>(); // Lấy component TextMeshProUGUI từ con của Button
            if (buttonText != null)
            {
                buttonText.text = "Upgrade"; // Cập nhật văn bản
            }
        }
    }
    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);  // Optional: Keep this object across scenes
        }
        else
        {
            Destroy(gameObject);
        }

        // Tắt tất cả các process indicator khi bắt đầu
        foreach (var image in buttonPressProcess)
        {
            image.gameObject.SetActive(false);
        }
    }
   
    // Gọi phương thức này khi người dùng nhìn vào nút
    public void RunButtonProcess(int buttonIndex)
    {
        if (currentProcess != null)
        {
            StopCoroutine(currentProcess);
        }
        currentProcess = StartCoroutine(ButtonProcess(buttonIndex));
    }

    // Coroutine thực hiện hiển thị quá trình và kích hoạt nút sau 1 giây
    private IEnumerator ButtonProcess(int buttonIndex)
    {
        buttonPressProcess[buttonIndex].gameObject.SetActive(true);
        buttonPressProcess[buttonIndex].fillAmount = 0f;

        float time = 0f;
        while (time < 1f)
        {
            time += Time.deltaTime;
            buttonPressProcess[buttonIndex].fillAmount = time / 1f;
            yield return null;
        }

        buttons[buttonIndex].onClick.Invoke();  // Kích hoạt sự kiện của nút
        buttonPressProcess[buttonIndex].gameObject.SetActive(false);
    }

    // Gọi phương thức này để hủy tiến trình nếu người dùng dời mắt đi
    public void CancelProcess(int buttonIndex)
    {
        if (currentProcess != null)
        {
            StopCoroutine(currentProcess);
            buttonPressProcess[buttonIndex].gameObject.SetActive(false);
        }
    }
    // Thay đổi màu sắc của tất cả các nút thành màu đỏ
    public void ButtonRedColor()
    {
        foreach (Button button in buttons)
        {
            var image = button.GetComponent<Image>(); // Lấy component Image của nút
            if (image != null)
            {
                image.color = Color.red; // Đặt màu của Image thành đỏ
            }
        }
    }

    // Thay đổi màu sắc của Image trên mỗi nút thành màu hồng
    public void ButtonPinkColor()
    {
        foreach (Button button in buttons)
        {
            var image = button.GetComponent<Image>(); // Lấy component Image của nút
            if (image != null)
            {
                image.color = Color.magenta; // Đặt màu của Image thành magenta
            }
        }
    }
}
