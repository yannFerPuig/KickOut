using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class LouisStats : FighterStats
{
    public void Initialize()
    {
        maxHealth = 80;

        width = 0.3f;
        height = 0.83f;

        damage = 12;
        defense = 7;
        attackRange = 2.2f;

        moveSpeed = 4;

        jumpForce = 30;
        gravityScale = 10;
        fallingGravityScale = 40;
        groundCheckRadius = 0.5f;

        capsuleCollider2D = gameObject.GetComponent<CapsuleCollider2D>();
        attackPoint = GameObject.FindGameObjectWithTag("AttackPoint");
        attackPoint.transform.position = new Vector3(attackPointPos.x, attackPointPos.y, attackPointPos.z);
        groundCheck = GameObject.FindGameObjectWithTag("GroundCheck");
    }
}
