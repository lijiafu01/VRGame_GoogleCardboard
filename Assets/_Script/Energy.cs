using System.Collections;
using UnityEngine;

public class Energy : MonoBehaviour
{
    public float speed = 5f;  // Tốc độ di chuyển của energy

    void Start()
    {
        // Đảm bảo rằng Rigidbody không ảnh hưởng bởi lực hấp dẫn và sử dụng kiểu kinematic
        Rigidbody rb = GetComponent<Rigidbody>();
        if (rb != null)
        {
            rb.useGravity = false;
            rb.isKinematic = true;
        }
    }

    void Update()
    {
        // Di chuyển energy về phía "EnergyTower"
        GameObject energyTower = GameObject.FindGameObjectWithTag("EnergyTower");
        if (energyTower != null)
        {
            Vector3 direction = energyTower.transform.position - transform.position;
            transform.position += direction.normalized * speed * Time.deltaTime;
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        // Kiểm tra va chạm với "EnergyTower"
        if (other.gameObject.CompareTag("EnergyTower"))
        {
            ArriveAtTower();
        }
    }

    void ArriveAtTower()
    {
        ExpBar.Instance.UpdateExp(1);
        Destroy(gameObject);  // Xóa đối tượng energy khi nó đã đến tháp
    }
}
