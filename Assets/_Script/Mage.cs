using System.Collections;
using UnityEngine;

public class Mage : MonoBehaviour
{
    public Transform shootingPoint;
    public GameObject energyBallPrefab;  // Prefab của quả cầu năng lượng
    public float fireRate = 2f;  // Tần suất bắn mỗi giây
    public float radius = 50f;  // Bán kính tìm kiếm mục tiêu
    public float projectileSpeed = 10f;  // Tốc độ di chuyển của quả cầu năng lượng
    public float launchDelay = 0.5f;  // Thời gian chờ trước khi quả cầu bắn

    private Transform target;
    private float fireCooldown = 0;

    private Animator anim;
    private void Start()
    {
        anim = GetComponentInChildren<Animator>();
    }
    void Update()
    {
        if (target == null)
        {
            FindClosestEnemy();
        }

        if (fireCooldown <= 0 && target != null)
        {
            StartCoroutine(DelayedShoot());
            fireCooldown = 1 / fireRate;  // Đặt lại thời gian chờ cho lần bắn tiếp theo
        }
        else
        {
            fireCooldown -= Time.deltaTime;
        }
    }

    IEnumerator DelayedShoot()
    {
        anim.SetTrigger("attack");
        yield return new WaitForSeconds(0.3f);
        if (energyBallPrefab && shootingPoint && target)
        {
            GameObject energyBall = Instantiate(energyBallPrefab, shootingPoint.position, Quaternion.identity);
            Rigidbody rb = energyBall.GetComponent<Rigidbody>();

            // Tạm thời tắt vật lý bằng cách sử dụng isKinematic
            rb.isKinematic = true;  // Ngăn không cho quả cầu rơi xuống ngay lập tức

            yield return new WaitForSeconds(launchDelay);  // Đợi một khoảng thời gian trước khi bắn

            // Sau khi chờ đợi, cho phép vật lý tác động trở lại và áp dụng lực
            rb.isKinematic = false;  // Cho phép vật lý tác động lại vào quả cầu
            Vector3 direction = (target.position - shootingPoint.position).normalized;
            rb.velocity = direction * projectileSpeed;  // Áp dụng vận tốc cho quả cầu năng lượng
        }
    }


    void FindClosestEnemy()
    {
        Collider[] hitColliders = Physics.OverlapSphere(transform.position, radius, LayerMask.GetMask("Enemy"));
        float closestDistance = Mathf.Infinity;
        Transform closestEnemy = null;

        foreach (Collider hitCollider in hitColliders)
        {
            float distance = Vector3.Distance(transform.position, hitCollider.transform.position);
            if (distance < closestDistance)
            {
                closestDistance = distance;
                closestEnemy = hitCollider.transform;
            }
        }

        target = closestEnemy;  // Cập nhật mục tiêu gần nhất
    }
}
