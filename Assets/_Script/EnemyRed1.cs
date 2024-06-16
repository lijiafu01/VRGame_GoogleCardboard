using System.Collections;
using UnityEngine;
using UnityEngine.EventSystems;

public class EnemyRed1 : Enemy
{
    public GameObject EnergyPrefab;
    public Material frozenMaterial;  // Material khi bị đóng băng

    private GameObject _target = null;
    public bool canMove = true;
    public bool canAttack = false;
    
    public float attackRate = 1.5f;
    private float attackCooldown;

    private Rigidbody rb;
    private Animator animator;
    private Material originalMaterial;  // Lưu trữ material gốc
    private Renderer enemyBodyRenderer;  // Renderer của EnemyBody
    private Vector3 moveDirection = Vector3.forward;  // Hướng di chuyển cố định của kẻ thù
    private bool isFreeze = false;
    private void Start()
    {
        rb = GetComponent<Rigidbody>();
        animator = GetComponentInChildren<Animator>();
        // Giả sử EnemyBody là một đối tượng con có tag "EnemyBody"
        enemyBodyRenderer = transform.Find("EnemyBody").GetComponent<Renderer>();
        originalMaterial = enemyBodyRenderer.material;  // Lưu lại material ban đầu
    }

    private void FixedUpdate()
    {
        if (isFreeze) return;
        if (canMove)
        {
            MoveStraight();
        }
        if (canAttack)
        {
            Attack();
        }
        if (transform.position.y <= -1)
        {
            Destroy(gameObject);
        }
    }

    void MoveStraight()
    {
        rb.MovePosition(rb.position + moveDirection * speed * Time.fixedDeltaTime);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Wall"))
        {
            canMove = false;
            animator.SetTrigger("attack");
            _target = other.gameObject;
            canAttack = true;
        }
    }

    void Attack()
    {
        if (attackCooldown <= 0)
        {
            _target.GetComponent<Wall>().TakeDamage(damage);
            attackCooldown = attackRate;
        }
    }

    public override void TakeDamage(float amount)
    {
        health -= amount;
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
    public override void MageFreeze(float feezeTime)
    {
        StartCoroutine(Freeze(feezeTime));
    }
    IEnumerator Freeze(float feezeTime)
    {
        isFreeze = true;
        animator.SetTrigger("freeze");
        enemyBodyRenderer.material = frozenMaterial;  // Áp dụng material đóng băng

        yield return new WaitForSeconds(feezeTime);  // Đợi trong thời gian đóng băng
        animator.SetTrigger("run");
        isFreeze = false;
        enemyBodyRenderer.material = originalMaterial;  // Khôi phục material ban đầu
    }
}
