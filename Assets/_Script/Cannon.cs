using UnityEngine;

public class Cannon : MonoBehaviour
{
    public Transform shootingPoint;
    public GameObject bulletPrefab;
    public Transform target; // Mục tiêu để bắn
    public float fireRate = 1f;
    public float angle = 20f; // Góc bắn cố định
    public float radius = 50f; // Bán kính tìm kiếm kẻ thù

    private float fireCooldown = 0;

    private void Update()
    {
        if (target == null)
        {
            FindClosestEnemy();
        }
        else
        {
            // Xử lý quay pháo về hướng kẻ thù
            Vector3 targetPositionFlat = new Vector3(target.position.x, transform.position.y, target.position.z);
            Vector3 directionToTarget = targetPositionFlat - transform.position;
            Quaternion targetRotation = Quaternion.LookRotation(directionToTarget);
            transform.rotation = Quaternion.RotateTowards(transform.rotation, targetRotation, Time.deltaTime * 100); // Quay với tốc độ có thể điều chỉnh
        }

        if (fireCooldown <= 0)
        {
            Shoot();
            fireCooldown = 1 / fireRate;
        }
        else
        {
            fireCooldown -= Time.deltaTime;
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
        if (target == null) return; // Ensure there is a target

        GameObject bullet = Instantiate(bulletPrefab, shootingPoint.position, Quaternion.identity);
        Rigidbody rb = bullet.GetComponent<Rigidbody>();

        Vector3 toTarget = target.position - shootingPoint.position;
        toTarget.y = 0; // Ignore height differences
        float distance = toTarget.magnitude;
        float yOffset = target.position.y - shootingPoint.position.y;

        float angleRad = angle * Mathf.Deg2Rad;
        float gravity = Physics.gravity.magnitude;

        float initialVelocity = Mathf.Sqrt(gravity * distance * distance / (2 * Mathf.Cos(angleRad) * Mathf.Cos(angleRad) * (distance * Mathf.Tan(angleRad) - yOffset)));

        Vector3 forward = toTarget.normalized;
        Vector3 up = Mathf.Sin(angleRad) * Vector3.up;
        Vector3 force = (forward + up) * initialVelocity;

        rb.AddForce(force, ForceMode.Impulse);
    }
}
