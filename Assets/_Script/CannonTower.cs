using UnityEngine;

public class CannonTower : MonoBehaviour
{
    public Transform shootingPoint;
    public GameObject cannonBallPrefab;
    public float fireRate = 1f;
    public float radius = 50f;
    public float rotationSpeed = 10f;

    private Vector3 targetPosition;
    private float fireCooldown = 0;

    void Update()
    {
        transform.Rotate(0, rotationSpeed * Time.deltaTime, 0);  // Xoay liên tục

        if (fireCooldown <= 0)
        {
            if (FindClosestEnemy())
            {
                Shoot();
            }
            fireCooldown = 1 / fireRate;
        }
        else
        {
            fireCooldown -= Time.deltaTime;
        }
    }

    bool FindClosestEnemy()
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

        if (closestEnemy != null)
        {
            targetPosition = closestEnemy.position;
            return true;
        }
        return false;
    }

    void Shoot()
    {
        GameObject cannonBall = Instantiate(cannonBallPrefab, shootingPoint.position, Quaternion.identity);
        CannonTowerBullet bulletScript = cannonBall.GetComponent<CannonTowerBullet>();
        bulletScript.SetTargetPosition(targetPosition);
    }
}
