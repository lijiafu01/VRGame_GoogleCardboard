using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    public GameObject BossPrefab;
    public Transform leftBound;  // Biên trái để giới hạn phạm vi sinh
    public Transform rightBound; // Biên phải để giới hạn phạm vi sinh
    public GameObject enemyPrefab; // Prefab của kẻ thù
    public int numberOfEnemies = 5; // Số lượng kẻ thù trong một hàng
    public float spawnInterval = 10f; // Thời gian giữa các đợt sinh kẻ thù

    private float timer; // Đếm ngược thời gian để sinh đợt kẻ thù tiếp theo
    private float upgradeMultiplier = 1.0f; // Nhân số cho các thuộc tính kẻ thù khi nâng cấp
    private float moveMultiplier = 1.01f;
    int level = 1;
    void Start()
    {
        timer = spawnInterval; // Khởi tạo đồng hồ đếm ngược
    }

    void Update()
    {
        timer -= Time.deltaTime;
        if (timer <= 0)
        {
            SpawnEnemies();
            timer = spawnInterval; // Đặt lại đồng hồ cho đợt kế tiếp
        }
    }

    void SpawnEnemies()
    {
        float range = rightBound.position.x - leftBound.position.x;
        float spacing = range / (numberOfEnemies - 1); // Tính khoảng cách giữa các kẻ thù

        for (int i = 0; i < numberOfEnemies; i++)
        {
            float spawnX = leftBound.position.x + i * spacing; // Tính vị trí x cho kẻ thù
            Vector3 spawnPosition = new Vector3(spawnX, leftBound.position.y, leftBound.position.z);
            GameObject enemy = Instantiate(enemyPrefab, spawnPosition, Quaternion.identity);
            Enemy enemyScript = enemy.GetComponent<Enemy>();
            enemyScript.health *= upgradeMultiplier;
            enemyScript.speed *= moveMultiplier;
            enemyScript.damage *= upgradeMultiplier;
        }
        if (level % 10 == 0)
        {
            numberOfEnemies += 1;
        }

    }
    public void UpgradeEnemies()
    {
        upgradeMultiplier *= 1.05f;  // Tăng mỗi thuộc tính của kẻ thù lên 5%
        moveMultiplier *= 1.01f;
        level += 1;
        if(level == 10) { BossPrefab.SetActive(true); }
    }
}
