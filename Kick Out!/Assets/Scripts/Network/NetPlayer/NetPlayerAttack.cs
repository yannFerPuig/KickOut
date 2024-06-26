using System.Collections;
using System.Collections.Generic;
using Unity.Netcode;
using UnityEngine;

public class NetPlayerAttack : NetworkBehaviour
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

    void Start()
    {
        attackRange = stats.attackRange;
    }

    // Update is called once per frame
    void Update()
    {
        if (!IsOwner) return;
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
        Vector3 attack = new Vector3(attackPoint.position.x, attackPoint.position.y, attackPoint.position.z);
        Vector3 size = new Vector3(attackRange, 0.2f, 0);

        if (move.isFlipped) 
        {
            size.x *= -1;
            attack.x -= attackRange / 2;
        }
        else 
        {
            attack.x += attackRange / 2;
        }

        Collider2D[] enemiesHitted = Physics2D.OverlapBoxAll(attack, size, 90, enemyLayer);

        //Damage the enemy 
        foreach(var enemy in enemiesHitted)
        {
            enemy.GetComponent<IA>().TakeDamage(stats.damage);
        }
    }

    void Special() 
    {
        isAttacking = true;

        move.horizontalInput = 0f;

        StartCoroutine(MyFunctionAfterDelay(special.length));

        //Detect the enemies in range
        //OverlapCircleAll creates a 'circle' around a point (1st parameter) with a certain radius (2nd parameter) and you can apply layers (3rd parameter)
        Vector3 attack = new Vector3(attackPoint.position.x, attackPoint.position.y, attackPoint.position.z);
        Vector3 size = new Vector3(attackRange, 0.2f, 0);

        if (move.isFlipped) 
        {
            size.x *= -1;
            attack.x -= attackRange / 2;
        }
        else 
        {
            attack.x += attackRange / 2;
        }

        Collider2D[] enemiesHitted = Physics2D.OverlapBoxAll(attack, size, 90, enemyLayer);

        //Damage the enemy 
        foreach(var enemy in enemiesHitted)
        {
            enemy.GetComponent<IA>().TakeDamage(stats.damage);
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
        //Pour dessiner le cercle d'attaque

        if(attackPoint == null)
            return;

        Vector3 attack = new Vector3(attackPoint.position.x, attackPoint.position.y, attackPoint.position.z);
        Vector3 size = new Vector3(attackRange, 0.2f, 0);

        if (move.isFlipped) 
        {
            size.x *= -1;
            attack.x -= attackRange / 2;
        }
        else 
        {
            attack.x += attackRange / 2;
        }

        Gizmos.DrawCube(attack, size);
    }    
}
