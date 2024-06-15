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
    public SoundDesign soundManager;

    void Start()
    {
        stats = gameObject.GetComponent<FighterStats>();
        fighter = gameObject.GetComponent<Fighter>();
        animator = gameObject.GetComponent<Animator>();
        attackPoint = stats.attackPoint.transform;

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
        bool miss = true;

        Vector3 center = new Vector3(attackPoint.position.x, attackPoint.position.y, 0);
        Vector3 size = new Vector3(attackRange * 2, 0.25f, 0);

        Collider2D[] enemiesHitted = Physics2D.OverlapBoxAll(center, size, 0, enemyLayer);

        foreach (var enemy in enemiesHitted)
        {
            if (!enemy.GetComponent<PlayerMovement>().isBlocking)
            {
                Debug.Log("Caca");
                enemy.GetComponent<Fighter>().TakeDamage(stats.damage);
            }
            else 
            {
                enemy.GetComponent<FighterStats>().blockCD -= stats.reduceCD;
            }

            miss = false;
            
            //soundManager.PlaySFX(stats.punchSound);
        }
        if(miss)
        {
            //soundManager.PlaySFX(stats.missShot);
        }    
    }

    void Special() 
    {
        isAttacking = true;
        bool miss = true;
        Vector3 center = new Vector3(attackPoint.position.x, attackPoint.position.y, 0);
        Vector3 size = new Vector3(attackRange * 2, 0.25f, 0);

        Collider2D[] enemiesHitted = Physics2D.OverlapBoxAll(center, size, 0, enemyLayer);

        foreach (var enemy in enemiesHitted)
        {
            if (!enemy.GetComponent<PlayerMovement>().isBlocking)
            {
                Debug.Log("Caca");
                enemy.GetComponent<Fighter>().TakeDamage(stats.damage);
            }
            else 
            {
                enemy.GetComponent<FighterStats>().blockCD -= stats.reduceCD;
            }
        
            miss = false;
            //soundManager.PlaySFX(stats.specialSound);
        }

        if(miss)
        {
            //soundManager.PlaySFX(stats.missShot);
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