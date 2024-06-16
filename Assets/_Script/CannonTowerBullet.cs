using System.Collections;
using UnityEngine;

public class CannonTowerBullet : MonoBehaviour
{
    public float explosionRadius;
    public float upSpeed =50;
    public float downSpeed =50;

    public GameObject explosionEffect;
    public float upTime = 3f;
    public float teleportHeight = 30f;
    private Vector3 targetPosition;

    private Rigidbody rb;

    private void Start()
    {
        rb = GetComponent<Rigidbody>();
        StartCoroutine(Ascend());
    }

    public void SetTargetPosition(Vector3 newTargetPosition)
    {
        targetPosition = newTargetPosition;
    }

    IEnumerator Ascend()
    {
        transform.rotation = Quaternion.LookRotation(Vector3.up);  
        rb.velocity = Vector3.up * upSpeed;  // Đi lên với tốc độ nào đó
        yield return new WaitForSeconds(upTime);

        // Đặt lại vị trí và hướng đầu đạn pháo xuống đất khi bắt đầu rơi
        transform.position = new Vector3(targetPosition.x, teleportHeight, targetPosition.z);
        transform.rotation = Quaternion.LookRotation(Vector3.down);  // Xoay để hướng xuống
        rb.velocity = Vector3.down * downSpeed;  // Rơi xuống
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Ground") || collision.gameObject.CompareTag("Enemy"))
        {
            TriggerExplosion();
            Destroy(gameObject);
        }
    }

    void TriggerExplosion()
    {
        GameObject explosion = Instantiate(explosionEffect, transform.position, Quaternion.identity);
        PerformSphereCast();
    }
    void PerformSphereCast()
    {
        Collider[] hitColliders = Physics.OverlapSphere(transform.position, explosionRadius);
        foreach (var hitCollider in hitColliders)
        {
            if (hitCollider.CompareTag("Enemy"))
            {
                Enemy enemy = hitCollider.GetComponent<Enemy>();
                enemy.TakeDamage(200);

            }
        }
    }
}
