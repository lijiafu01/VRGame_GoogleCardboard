using UnityEngine;

public class Archer : MonoBehaviour
{
    public float attackRotationSpeed = 100f;  // Tốc độ quay về phía mục tiêu
    public Transform shootingPoint;
    public GameObject bulletPrefab;
    public Transform target;  // Mục tiêu để bắn
    public float fireRate = 1f;
    public float radius = 50f;  // Bán kính tìm kiếm kẻ thù
    public float shootForce = 50f;  // Lực bắn, có thể điều chỉnh trong Unity Editor
    private Animator anim;
    private float fireCooldown = 0;
    private bool isAligned = false;  // Kiểm tra xem đã quay đúng hướng chưa

    private void Start()
    {
        anim = GetComponentInChildren<Animator>();
    }

    private void Update()
    {
        if (target == null)
        {
            FindClosestEnemy();
        }
        else
        {
            AlignToTarget();
        }

        if (fireCooldown <= 0 && isAligned)
        {
            Shoot();
            fireCooldown = 1 / fireRate;
        }
        else
        {
            fireCooldown -= Time.deltaTime;
        }
    }

    void AlignToTarget()
    {
        Vector3 targetPositionFlat = new Vector3(target.position.x, transform.position.y, target.position.z);
        Vector3 directionToTarget = targetPositionFlat - transform.position;
        Quaternion targetRotation = Quaternion.LookRotation(directionToTarget);
        transform.rotation = Quaternion.RotateTowards(transform.rotation, targetRotation, Time.deltaTime * attackRotationSpeed);

        // Kiểm tra xem đã quay đúng hướng chưa
        if (Quaternion.Angle(transform.rotation, targetRotation) < 0.1f)
        {
            isAligned = true;
           
        }
        else
        {
            isAligned = false;
        }
    }
    void FindClosestEnemy()
    {
        Collider[] hitColliders = Physics.OverlapSphere(shootingPoint.position, radius, LayerMask.GetMask("Enemy"));
        float closestDistance = Mathf.Infinity;
        Transform closestEnemy = null;

        foreach (Collider hitCollider in hitColliders)
        {
            float distance = Vector3.Distance(shootingPoint.position, hitCollider.transform.position);
            if (distance < closestDistance)
            {
                closestDistance = distance;
                closestEnemy = hitCollider.transform;
            }
        }

        target = closestEnemy; // Set the closest enemy as the target
    }
    void Shoot()
    {
        if (target == null || !isAligned) return;  // Đảm bảo có mục tiêu và đã định hướng đúng

        Vector3 toTarget = target.position - shootingPoint.position;
        Quaternion rotation = Quaternion.LookRotation(toTarget);

        GameObject bullet = Instantiate(bulletPrefab, shootingPoint.position, rotation);
        Rigidbody rb = bullet.GetComponent<Rigidbody>();

        Vector3 force = toTarget.normalized * shootForce;  // Áp dụng lực đã điều chỉnh

        rb.AddForce(force, ForceMode.VelocityChange);
        isAligned = false;  // Reset trạng thái định hướng sau khi bắn
        anim.SetTrigger("attack");  // Kích hoạt animation bắn
    }
}
