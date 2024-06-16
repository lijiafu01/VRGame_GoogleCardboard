using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ExpBar : MonoBehaviour
{
    public TMP_Text levelText;

    private int level = 1;
    public EnemySpawner enemySpawner;
    public static ExpBar Instance { get; private set; }  // Singleton instance

    [SerializeField] private float expMax = 100;
    [SerializeField] private int currentExp = 0;

    public Image ExpBarImage;  // Tham chiếu đến UI Image được sử dụng làm thanh máu

    private void Awake()
    {
        // Đảm bảo chỉ có một instance của ExpBar
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);  // Optional: Keep this object alive when loading new scenes
        }
        else
        {
            Destroy(gameObject);
        }
    }
    public void Upgraded()
    {
        currentExp = 0;
        expMax *= 1.1f;
        enemySpawner.UpgradeEnemies();
        ShopManager.Instance.ButtonRedColor();
        level++;
        levelText.text = "Level " + level.ToString();

    }
    public bool CanUpdate()
    {
        if ((currentExp >= expMax))
        {
            return true;
        }
        return false;
    }
    private void Start()
    {
        UpdateExpBar();
    }

    public void UpdateExp(int exp = 1)
    {
        currentExp += exp;
        UpdateExpBar();
    }

    private void UpdateExpBar()
    {
        
        float expPercent = (float)currentExp / expMax;  // Cast to float to ensure decimal division
        ExpBarImage.fillAmount = expPercent;
        if(currentExp >= expMax) { ShopManager.Instance.ButtonPinkColor(); }
    }
}
