using System.Collections;
using UnityEngine;

public abstract class Enemy : MonoBehaviour
{
    public float health = 100f;
    public float speed = 3f;
    public float damage = 1f;
    public abstract void TakeDamage(float amount); // Phương thức trừu tượng để các lớp con phải thực thi

    // Phương thức không trừu tượng có thể được triển khai trong lớp base
    public virtual void MageFreeze(float freezeTime)
    {
        
    } 
}
