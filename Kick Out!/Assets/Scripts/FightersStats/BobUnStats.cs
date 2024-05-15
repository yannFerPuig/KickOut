using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class BobUnStats : FighterStats
{
    public void Initialize()
    {
        maxHealth = 100;

        width = 0.3f;
        height = 0.83f;

        damage = 7;
        defense = 12;
        attackRange = 1.18f;

        moveSpeed = 3;

        jumpForce = 15;
        gravityScale = 10;
        fallingGravityScale = 50;
        groundCheckRadius = 0.5f;

        capsuleCollider2D = gameObject.GetComponent<CapsuleCollider2D>();
        attackPoint = GameObject.FindGameObjectWithTag("AttackPoint");
        attackPoint.transform.position = new Vector3(attackPointPos.x, attackPointPos.y, attackPointPos.z);
        groundCheck = GameObject.FindGameObjectWithTag("GroundCheck");
    }
}
