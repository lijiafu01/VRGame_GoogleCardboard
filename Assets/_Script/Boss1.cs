using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class Boss1 : Enemy
{
    public Image bossHpBar;
    public Transform shootPoint;
    public GameObject BossWeapon;
    public GameObject BossWeaponPrefab;
    public GameObject EnergyPrefab;
    public Material frozenMaterial;  // Material khi bị đóng băng

    private GameObject _target = null;
    public bool canMove = true;
    public float attackInterval = 8f;  // Thời gian giữa các lần tấn công
    private float attackTimer;  // Đếm ngược thời gian đến lần tấn công tiếp theo

    private Rigidbody rb;
    private Animator animator;
    private Material originalMaterial;  // Lưu trữ material gốc
    private Renderer enemyBodyRenderer;  // Renderer của EnemyBody
    private Vector3 moveDirection = Vector3.forward;  // Hướng di chuyển cố định của kẻ thù
    private bool isFreeze = false;
    public float MaxHp = 3000;
    private void UpdateHpBar()
    {

        float hpPercent = (float)health / MaxHp;  // Cast to float to ensure decimal division
        bossHpBar.fillAmount = hpPercent;
    }
    private void OnEnable()
    {

        health = MaxHp;
        rb = GetComponent<Rigidbody>();
        animator = GetComponentInChildren<Animator>();
        // Giả sử EnemyBody là một đối tượng con có tag "EnemyBody"
        enemyBodyRenderer = transform.Find("EnemyBody").GetComponent<Renderer>();
        originalMaterial = enemyBodyRenderer.material;  // Lưu lại material ban đầu
        attackTimer = attackInterval;  // Khởi tạo đồng hồ đếm ngược
    }

    private void FixedUpdate()
    {
        if (isFreeze) return;

        if (canMove)
        {
            MoveStraight();
        }

        attackTimer -= Time.fixedDeltaTime;
        if (attackTimer <= 0)
        {
            StartCoroutine(Attack());
            attackTimer = attackInterval; // Đặt lại đồng hồ cho lần kế tiếp
        }

        if (transform.position.y <= -1)
        {
            Destroy(gameObject);
        }
    }

    void MoveStraight()
    {
        rb.MovePosition(rb.position + moveDirection * speed * Time.fixedDeltaTime);
        if(transform.position.z <=60f) { canMove = false; }
    }

    /* void Attack()
     {
         animator.SetTrigger("attack");
         *//*if (_target != null)
         {
             _target.GetComponent<Wall>().TakeDamage(damage);
         }*//*
     }*/
    public IEnumerator Attack()
    {
        animator.SetTrigger("attack");
        yield return new WaitForSeconds(2.5f);  // Delay before firing the weapon
        if (BossWeaponPrefab && shootPoint)
        {
            GameObject weapon = Instantiate(BossWeaponPrefab, shootPoint.position, Quaternion.identity);
            Rigidbody weaponRb = weapon.GetComponent<Rigidbody>();
            if (weaponRb)
            {
                Vector3 forceDirection = - transform.forward;  // Assume the boss shoots forward
                weaponRb.AddForce(forceDirection * 100, ForceMode.Impulse);
            }
        }
        BossWeapon.gameObject.SetActive(false);
        yield return new WaitForSeconds(3f);
        BossWeapon.gameObject.SetActive(true);
    }
    public override void TakeDamage(float amount)
    {

        health -= amount;
        UpdateHpBar();
        if (health <= 0)
        {
            Die();
        }
    }

    void Die()
    {
        Instantiate(EnergyPrefab, transform.position, Quaternion.identity);
        Destroy(gameObject);
    }

    public override void MageFreeze(float freezeTime)
    {
        StartCoroutine(Freeze(freezeTime));
    }

    IEnumerator Freeze(float freezeTime)
    {
        isFreeze = true;
        animator.SetTrigger("freeze");
        enemyBodyRenderer.material = frozenMaterial;  // Áp dụng material đóng băng

        yield return new WaitForSeconds(freezeTime);  // Đợi trong thời gian đóng băng

        animator.SetTrigger("run");
        isFreeze = false;
        enemyBodyRenderer.material = originalMaterial;  // Khôi phục material ban đầu
    }
}
