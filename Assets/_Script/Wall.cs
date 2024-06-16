using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Wall : MonoBehaviour
{
    public float Health = 100;
    public float MaxHealth = 100;
    public void TakeDamage(float damage)
    {
        Health -= damage;
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("BossWeapon"))
        {
            Health -= 30;
        }
    }
}