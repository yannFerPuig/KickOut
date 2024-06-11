using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AIAttack : Attack
{
    // SCRIPTS
    public FighterStats stats;
    public Fighter fighter;

    // COMPONENTS
    public Animator animator;
    public Transform attackPoint;
    
    // EXTRAS
    public LayerMask enemyLayer;

    // DATA
    public float attackRange;
    public float attackSpeed;
    public bool isAttacking;
    public float attackCooldown = 1f;
    private float attackCooldownTimer;

    void Start()
    {
        stats = gameObject.GetComponent<FighterStats>();
        fighter = gameObject.GetComponent<Fighter>();
        animator = gameObject.GetComponent<Animator>();
        attackPoint = gameObject.transform.Find("AttackPoint");

        attackRange = stats.attackRange;
        enemyLayer = LayerMask.GetMask(LayerMask.LayerToName(fighter.enemy.layer));
        attackCooldownTimer = attackCooldown;
    }

    void Update()
    {
        attackCooldownTimer -= Time.deltaTime;

        if (attackCooldownTimer <= 0)
        {
            PerformAttack();
            attackCooldownTimer = attackCooldown;
        }
    }

    void PerformAttack()
    {
        animator.SetTrigger("Attack");
    }

    void Attack() 
    {
        isAttacking = true;

        Vector3 center = new Vector3(attackPoint.position.x, attackPoint.position.y, 0);
        Vector3 size = new Vector3(attackRange * 2, 0.25f, 0);

        Collider2D[] enemiesHitted = Physics2D.OverlapBoxAll(center, size, 0, enemyLayer);

        foreach (var enemy in enemiesHitted)
        {
            enemy.GetComponent<Fighter>().TakeDamage(stats.damage);

            if (enemy.CompareTag("Player"))
            {
                if (enemy.GetComponent<PlayerMovement>().isBlocking)
                {
                    enemy.GetComponent<FighterStats>().blockCD -= stats.reduceCD;
                }
            }
        }
    }

    IEnumerator MyFunctionAfterDelay(float delay)
    {
        yield return new WaitForSeconds(delay);
        isAttacking = false;
    }

    void OnDrawGizmosSelected()
    {
        if (attackPoint == null)
            return;

        Gizmos.color = Color.red;
        Vector3 center = new Vector3(attackPoint.position.x, attackPoint.position.y, 0);
        Vector3 size = new Vector3(attackRange * 2, 0.25f, 0);
        Gizmos.DrawCube(center, new Vector3(size.x, size.y, 1)); 
    }
}