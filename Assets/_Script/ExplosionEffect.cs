using UnityEngine;

public class ExplosionEffect : MonoBehaviour
{
    public float duration = 2f;  // Thời gian tồn tại của hiệu ứng

    void Start()
    {
        // Kích hoạt tất cả các Particle Systems khi tạo hiệu ứng
        foreach (ParticleSystem ps in GetComponentsInChildren<ParticleSystem>())
        {
            ps.Play();
        }

        Destroy(gameObject, duration); // Hủy hiệu ứng sau thời gian xác định
    }
}
