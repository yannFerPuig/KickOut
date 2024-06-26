using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IAAttack : MonoBehaviour
{
    //SCRIPTS
    public FighterStats stats;

    //COMPONENTS
    public Transform attackPoint;

    //DATA
    public float attackDamage;
    public float attackRange;
    public LayerMask attackMask;

    // Start is called before the first frame update
    void Start()
    {
        attackDamage = stats.damage;
        attackRange = stats.attackRange;        
    }

    public void Attack()
    {
        //We create a circle of center the position of the attack point and a radius of the attack range
        //We save in an array all the colliders that respect the attackMask
        Collider2D[] hitEnemy = Physics2D.OverlapCircleAll(attackPoint.position, attackRange, attackMask);

        //Damage the enemy 
        foreach(var enemy in hitEnemy)
        {
            enemy.GetComponent<Player>().TakeDamage(attackDamage);
        }
    }

    void OnDrawGizmosSelected()
    {
        //Pour dessiner le cercle d'attaque

        if(attackPoint == null)
            return;

        Gizmos.DrawWireSphere(attackPoint.position, attackRange);
    }
}
