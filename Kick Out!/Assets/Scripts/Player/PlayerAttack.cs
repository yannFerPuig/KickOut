using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAttack : Attack
{
    //SCRIPTS
    public FighterStats stats;
    public PlayerMovement move;
    public Fighter fighter;
    public SoundDesign soundManager;

    //COMPONENTS
    public Animator animator;
    public Transform attackPoint;
    
    //EXTRAS
    public LayerMask enemyLayer;

    //DATA
    public float attackRange;
    public float attackSpeed;
    public bool isAttacking;

    void Start()
    {
        stats = gameObject.GetComponent<FighterStats>();
        move = gameObject.GetComponent<PlayerMovement>();
        fighter = gameObject.GetComponent<Fighter>();
        soundManager = GameObject.Find("SoundManager").GetComponent<SoundDesign>();

        animator = gameObject.GetComponent<Animator>();

        attackPoint = gameObject.transform.Find("AttackPoint");

        attackRange = stats.attackRange;
        
        enemyLayer = LayerMask.GetMask(LayerMask.LayerToName(fighter.enemy.layer));
    }

    void Update()
    {
        if(Input.GetKeyDown(KeyCode.X))    
        {
            animator.SetTrigger("Attack");
        }

        if(Input.GetKeyDown(KeyCode.Z))    
        {
            animator.SetTrigger("Special");
        }
    }

    void Attack() 
    {
        isAttacking = true;

        move.horizontalInput = 0f;

        bool miss = true;

        StartCoroutine(MyFunctionAfterDelay(punch.length));

        //Detect the enemies in range
        //OverlapCircleAll creates a 'circle' around a point (1st parameter) with a certain radius (2nd parameter) and you can apply layers (3rd parameter)
        Vector3 center = new Vector3(attackPoint.position.x, attackPoint.position.y, 0);
        Vector3 size = new Vector3(attackRange * 2, 0.25f, 0);

        Collider2D[] enemiesHitted = Physics2D.OverlapBoxAll(center, size, 0, enemyLayer);

        Debug.Log(enemiesHitted.Length);

        //Damage the enemy 
        foreach(var enemy in enemiesHitted)
        {
            enemy.GetComponent<Fighter>().TakeDamage(stats.damage);

            if (enemy.tag == "Player" || enemy.tag == "Player1" || enemy.tag == "Player2")
            {
                if (!enemy.GetComponent<PlayerMovement>().isBlocking)
                {
                    enemy.GetComponent<Fighter>().TakeDamage(stats.damage);
                }
                else 
                {
                    enemy.GetComponent<FighterStats>().blockCD -= stats.reduceCD;
                }
            }
            else if (enemy.tag == "AI")
            {
                if (!enemy.GetComponent<AIMovement>().isBlocking)
                {
                    enemy.GetComponent<Fighter>().TakeDamage(stats.damage);
                }
            }

            miss = false;

            if (soundManager != null)
            {
                soundManager.PlaySFX(stats.punchSound);
            }
        }

        if (miss)
        {
         //   soundManager.PlaySFX(stats.missSound);
        }

    }

    void Special() 
    {
        isAttacking = true;

        move.horizontalInput = 0f;

        bool miss = true;

        StartCoroutine(MyFunctionAfterDelay(special.length));

        //Detect the enemies in range
        //OverlapCircleAll creates a 'circle' around a point (1st parameter) with a certain radius (2nd parameter) and you can apply layers (3rd parameter)
        Vector3 center = new Vector3(attackPoint.position.x, attackPoint.position.y, 0);
        Vector3 size = new Vector3(attackRange * 2, 0.25f, 0);

        Collider2D[] enemiesHitted = Physics2D.OverlapBoxAll(center, size, 0, enemyLayer);

        //Damage the enemy 
        foreach(var enemy in enemiesHitted)
        {
            enemy.GetComponent<Fighter>().TakeDamage(stats.specialDamage);

            //SFX

            if (enemy.tag == "Player" || enemy.tag == "Player1" || enemy.tag == "Player2")
            {
                if (!enemy.GetComponent<PlayerMovement>().isBlocking)
                {
                    enemy.GetComponent<Fighter>().TakeDamage(stats.damage);
                }
            }
            else if (enemy.tag == "AI")
            {
                if (!enemy.GetComponent<AIMovement>().isBlocking)
                {
                    enemy.GetComponent<Fighter>().TakeDamage(stats.damage);
                }
            }

            miss = false;

            if (soundManager != null)
            {
                soundManager.PlaySFX(stats.specialSound);
            }
        }

        if (miss)
        {
            //soundManager.PlaySFX(stats.missSound);
        }
    }

    IEnumerator MyFunctionAfterDelay(float delay)
    {
        //Cette fonction permet de déclencher une action après un certain temps (en lien avec StartCoroutine())
        //Gére le délai entre les attaques de bases pour éviter de lancer plusieurs attaques à la fois

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

        //Gizmos.DrawCube(center, new Vector3(size.x, size.y, 1)); 

        Gizmos.color = Color.blue;
        Gizmos.DrawCube(stats.center.transform.position, new Vector3(1f, 1f, 1f));
    }
}
