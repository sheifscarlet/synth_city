using System.Collections;
using UnityEngine;

public class MeeleEnemyCombat : MonoBehaviour
{
    [SerializeField] private MeeleEnemyController enemyController;
    public int damageAmount = 10;
    public float attackCooldown = 2f;
    private bool isAttacking = false;
    private float attackTimer = 0f;

    
    [SerializeField] private LayerMask playerLayer;

    private Animator animator;

    void Start()
    {
        animator = GetComponent<Animator>();
    }

    void Update()
    {
        attackTimer += Time.deltaTime;
        if (attackTimer >= attackCooldown)
        {
            isAttacking = false;
            attackTimer = 0f;
        }
    }

    void OnTriggerStay2D(Collider2D other)
    {
        if (other.CompareTag("Player") && !isAttacking)
        {
            StartCoroutine(Attack());
        }
    }

    IEnumerator Attack()
    {
        enemyController.enabled = false;
        isAttacking = true;
        animator.SetBool("Attack", true);
        PlayerHealthController.instance.PlayerTakeDamage(damageAmount);
        yield return new WaitForSeconds(0.3f); 
        animator.SetBool("Attack", false);
        enemyController.enabled = true;
        isAttacking = false;
    }

    
}