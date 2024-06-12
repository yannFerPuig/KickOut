using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAttack : MonoBehaviour
{
    //SCRIPTS
    public FighterStats stats;
    public PlayerMovement move;
    public StartRoundTimer startRoundTimer;

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

    // PLACE THE SOUND DIRECTORY
    public SoundDesign soundManager;

    void Start()
    {
        stats = gameObject.GetComponent<FighterStats>();
        move = gameObject.GetComponent<PlayerMovement>();
        startRoundTimer = GameObject.FindGameObjectWithTag("Canvas").GetComponent<StartRoundTimer>();

        animator = gameObject.GetComponent<Animator>();
        attackPoint = gameObject.transform.Find("AttackPoint");

        attackRange = stats.attackRange;
    }

    // Update is called once per frame
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
        Vector3 attack = new Vector3(attackPoint.position.x, attackPoint.position.y, attackPoint.position.z);
        Vector3 size = new Vector3(attackRange, 0.2f, 0);

        Collider2D[] enemiesHitted = Physics2D.OverlapBoxAll(attack, size, 90, enemyLayer);

        //Damage the enemy 
        foreach(var enemy in enemiesHitted)
        {
            enemy.GetComponent<Fighter>().TakeDamage(stats.damage);
            miss = false;
            if (enemy.CompareTag("Player"))
            {
                if (enemy.GetComponent<PlayerMovement>().isBlocking)
                {
                    enemy.GetComponent<FighterStats>().blockCD -= stats.reduceCD;
                }
                soundManager.PlaySFX(stats.punchSounds[UnityEngine.Random.Range(0, stats.punchSounds.Count - 1)]);
            }
        }

        if (miss)
        {
            soundManager.PlaySFX(Resources.Load<AudioClip>("Sound/missedShot"));
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
        Vector3 attack = new Vector3(attackPoint.position.x, attackPoint.position.y, attackPoint.position.z);
        Vector3 size = new Vector3(attackRange, 0.2f, 0);

        Collider2D[] enemiesHitted = Physics2D.OverlapBoxAll(attack, size, 90, enemyLayer);

        //Damage the enemy 
        foreach(var enemy in enemiesHitted)
        {
            enemy.GetComponent<Fighter>().TakeDamage(stats.specialDamage);
            miss = false;
            if (enemy.CompareTag("Player"))
            {
                if (enemy.GetComponent<PlayerMovement>().isBlocking)
                {
                    enemy.GetComponent<FighterStats>().blockCD -= stats.specialReduceCD;
                   
                }
                soundManager.PlaySFX(stats.specialSounds[UnityEngine.Random.Range(0, stats.specialSounds.Count-1)]);
            }
        }

        if (miss)
        {
            soundManager.PlaySFX(Resources.Load<AudioClip>("Sound/missedShot"));
        }
    }

    IEnumerator MyFunctionAfterDelay(float delay)
    {
        //Cette fonction permet de déclencher une action après un certain temps (en lien avec StartCoroutine())
        //Gére le délai entre les attaques de bases pour éviter de lancer plusieurs attaques à la fois

        yield return new WaitForSeconds(delay);

        isAttacking = false;
    }
}
