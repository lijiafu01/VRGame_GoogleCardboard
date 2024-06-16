using UnityEngine;

public class CannonBullet : MonoBehaviour
{
    [SerializeField] private AudioSource audioSource;
    [SerializeField] private GameObject explosionEffectPrefab; // Prefab chứa hiệu ứng nổ
    [SerializeField] private float explosionRadius = 5f; // Bán kính của vụ nổ
    private bool hasExploded = false; // Để đảm bảo rằng vụ nổ chỉ xảy ra một lần

    private void OnCollisionEnter(Collision collision)
    {
        if ((collision.gameObject.CompareTag("Ground")|| collision.gameObject.CompareTag("Enemy")) && !hasExploded)
        {
            TriggerExplosion();
            Destroy(gameObject); // Hủy đạn ngay lập tức
        }
    }

    private void TriggerExplosion()
    {
        hasExploded = true;
        //PlaySound();
        GameObject explosion = Instantiate(explosionEffectPrefab, transform.position, Quaternion.identity);
        PerformSphereCast();
    }

    private void PlaySound()
    {
        if (!audioSource.isPlaying)
        {
            audioSource.Play();
            audioSource.PlayOneShot(audioSource.clip);  // Đảm bảo âm thanh được phát
        }
    }

    void PerformSphereCast()
    {
        Collider[] hitColliders = Physics.OverlapSphere(transform.position, explosionRadius);
        foreach (var hitCollider in hitColliders)
        {
            if (hitCollider.CompareTag("Enemy"))
            {
                Enemy enemy = hitCollider.GetComponent<Enemy>();
                enemy.TakeDamage(150);

            }
        }
    }
}
