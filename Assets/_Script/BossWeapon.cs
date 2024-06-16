using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossWeapon : MonoBehaviour
{
    Rigidbody rb;
    private void Start()
    {
        rb = GetComponent<Rigidbody>();
    }
    private void OnTriggerEnter(Collider other)
    {
        
        if (other.gameObject.CompareTag("Wall2")) // Sử dụng CompareTag cho hiệu suất tốt hơn
        {
            rb.velocity = Vector3.zero; // Đặt vận tốc về 0 để dừng chuyển động
            rb.angularVelocity = Vector3.zero; // Đặt vận tốc góc về 0 nếu bạn cũng muốn dừng quay
            rb.isKinematic = true; // Đặt Rigidbody thành kinematic để ngăn chặn mọi chuyển động do vật lý

            //GameObject cameraObject = GameObject.FindGameObjectWithTag("MainCamera");

            /*CameraShake cameraShake = cameraObject.GetComponent<CameraShake>();
            StartCoroutine(cameraShake.Shake(0.5f, 0.5f));*/
            
            Destroy(gameObject, 2.9f);

        }
    }

}
