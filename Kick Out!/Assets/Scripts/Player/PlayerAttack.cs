using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAttack : MonoBehaviour
{
    //SCRIPTS
    public FighterStats stats;
    public PlayerMovement move;
    public RoundTimer startRoundTimer;
    public MainMenu menu;
    public Fighter fighter;

    //COMPONENTS
    public Animator animator;
    public Transform attackPoint;
    
    //EXTRAS
    public AnimationClip punch;
    public AnimationClip special;
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

        menu = GameObject.FindGameObjectWithTag("MainCamera").GetComponent<MainMenu>();
        
        if (menu.gameMode != "tutorial")
        {
            startRoundTimer = GameObject.FindGameObjectWithTag("Canvas").GetComponent<RoundTimer>();
        }

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

            if (enemy.CompareTag("Player"))
            {
                if (enemy.GetComponent<PlayerMovement>().isBlocking)
                {
                    enemy.GetComponent<FighterStats>().blockCD -= stats.reduceCD;
                }
            }
        }
    }

    void Special() 
    {
        isAttacking = true;

        move.horizontalInput = 0f;

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

            if (enemy.CompareTag("Player"))
            {
                if (enemy.GetComponent<PlayerMovement>().isBlocking)
                {
                    enemy.GetComponent<FighterStats>().blockCD -= stats.specialReduceCD;
                }
            }
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

        Gizmos.DrawCube(center, new Vector3(size.x, size.y, 1)); 
    }
}
