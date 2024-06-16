using UnityEngine;
using UnityEngine.UI;

public class WallHealthBar : MonoBehaviour
{
    public Wall wallHealthSystem;  // Tham chiếu đến HealthSystem của Wall
    public Image healthBarImage;  // Tham chiếu đến UI Image được sử dụng làm thanh máu

    void Update()
    {
        if (wallHealthSystem != null && healthBarImage != null)
        {
            float healthPercent = wallHealthSystem.Health / wallHealthSystem.MaxHealth;
            healthBarImage.fillAmount = healthPercent;  // Cập nhật fillAmount của Image dựa vào tỷ lệ máu hiện tại
        }
    }
}
