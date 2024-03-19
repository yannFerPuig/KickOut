using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAttack : MonoBehaviour
{
    //SCRIPTS
    public FighterStats stats;
    public PlayerMovement move;

    //COMPONENTS
    public Animator animator;
    public Transform attackPoint;
    
    //EXTRAS
    public AnimationClip punch;
    public LayerMask enemyLayer;

    //DATA
    public float attackRange;
    public float attackSpeed;
    public bool isAttacking;

    void Start()
    {
        attackRange = stats.attackRange.GetValue();    
        attackSpeed = stats.attackSpeed.GetValue();
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetKeyDown(KeyCode.X))    
        {
            move.horizontalInput = 0f;
            Attack();
        }
    }

    void Attack() 
    {
        isAttacking = true;

        StartCoroutine(MyFunctionAfterDelay(punch.length));

        //Play attack animation
        animator.SetTrigger("Attack");

        //Detect the enemies in range
        //OverlapCircleAll creates a 'circle' around a point (1st parameter) with a certain radius (2nd parameter) and you can apply layers (3rd parameter)
        Collider2D[] hitEnemy = Physics2D.OverlapCircleAll(attackPoint.position, attackRange, enemyLayer);

        //Damage the enemy 
        foreach(var enemy in hitEnemy)
        {
            enemy.GetComponent<FighterStats>().TakeDamage(stats.damage.GetValue());
        }
    }

    void OnDrawGizmosSelected()
    {
        if(attackPoint == null)
            return;

        Gizmos.DrawWireSphere(attackPoint.position, attackRange);   
    }

    IEnumerator MyFunctionAfterDelay(float delay)
    {
        //Cette fonction permet de déclencher une action après un certain temps (en lien avec StartCoroutine())
        //Gére le délai entre les attaques de bases pour éviter de lancer plusieurs attaques à la fois

        yield return new WaitForSeconds(delay);

        isAttacking = false;
        
    }
}
