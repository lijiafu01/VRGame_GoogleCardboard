using System.Collections.Generic;
using UnityEngine;
[System.Serializable]
public class Character
{
    

    public Type type; // Loại nhân vật
    public GameObject[] characters; // Mảng các nhân vật của mỗi loại
}
public enum Type
{
    Cannon,
    Archer,
    Mage,
    CannonTower
}
public class CharacterManager : MonoBehaviour
{
    public float upgradeMultiplier = 1.2f;

    public List<Character> characters; // Danh sách các loại nhân vật
    public static CharacterManager Instance { get; private set; } // Singleton instance

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject); // Đảm bảo không bị hủy khi load scene mới
        }
        else
        {
            Destroy(gameObject);
        }
    }

    // Phương thức để mua/kích hoạt nhân vật
    public void UpgradeCharacter(Type type,int buttonIndex)
    {
        if (!ExpBar.Instance.CanUpdate()) return;
        ExpBar.Instance.Upgraded();
        Character character = characters.Find(c => c.type == type); // Tìm nhân vật theo loại

        if (character != null && !IsCharacterMax(type))
        {
            foreach (GameObject obj in character.characters)
            {
                if (!obj.activeSelf) // Kiểm tra xem object đã được kích hoạt chưa
                {
                    obj.SetActive(true); // Kích hoạt object
                    break; // Dừng vòng lặp sau khi kích hoạt nhân vật đầu tiên chưa được kích hoạt
                }
            }
            if(IsCharacterMax(type))
            {
                ShopManager.Instance.UpdateText(buttonIndex);
            }
        }
        else
        {
            // Nâng cấp các thuộc tính cụ thể dựa vào loại nhân vật
            foreach (GameObject obj in character.characters)
            {
                switch (type)
                {
                    case Type.Cannon:
                        Cannon cannonScript = obj.GetComponent<Cannon>();
                        cannonScript.fireRate *= upgradeMultiplier;
                        break;
                    case Type.Archer:
                        Archer archerScript = obj.GetComponent<Archer>();
                        archerScript.fireRate *= upgradeMultiplier;
                        archerScript.shootForce *= upgradeMultiplier;
                        archerScript.attackRotationSpeed *= upgradeMultiplier;
                        break;
                    case Type.Mage:
                        Mage mageScript = obj.GetComponent<Mage>();
                        mageScript.radius *= upgradeMultiplier;
                        mageScript.fireRate *= upgradeMultiplier;
                        mageScript.projectileSpeed *= upgradeMultiplier;
                        break;
                    case Type.CannonTower:
                        CannonTower towerScript = obj.GetComponent<CannonTower>();
                        towerScript.fireRate *= upgradeMultiplier;
                        break;
                }
            }
        }
        
    }
    public bool IsCharacterMax(Type type)
    {
        Character character = characters.Find(c => c.type == type); // Tìm nhân vật theo loại

        if (character != null)
        {
            foreach (GameObject obj in character.characters)
            {
                if (!obj.activeSelf) // Nếu có bất kỳ đối tượng nào chưa được kích hoạt
                {
                    return false; // Trả về false ngay lập tức
                }
            }
            return true; // Tất cả đối tượng đã được kích hoạt
        }
        return false; // Không tìm thấy nhân vật của loại này hoặc có lỗi xảy ra
    }
}
