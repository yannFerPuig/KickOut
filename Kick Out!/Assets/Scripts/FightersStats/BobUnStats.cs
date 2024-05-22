using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class BobUnStats : FighterStats
{
    public void Initialize()
    {
        maxHealth = 140;

        width = 0.3f;
        height = 0.83f;

        damage = 6;
        defense = 12;
        attackRange = 1.18f;

        moveSpeed = 3;

        jumpForce = 28;
        gravityScale = 10;
        fallingGravityScale = 50;
        groundCheckRadius = 0.5f;

        spawnPoint = new Vector3(-8.5f, -2f, 0f);
        groundCheckPointPos = new Vector3(0, -0.3f, 0);
        attackPointPos = new Vector3(0, 0.26f, 0);

        capsuleCollider2D = gameObject.GetComponent<CapsuleCollider2D>();
        attackPoint = GameObject.FindGameObjectWithTag("AttackPoint");
        attackPoint.transform.position = new Vector3(attackPointPos.x, attackPointPos.y, attackPointPos.z);
        groundCheck = GameObject.FindGameObjectWithTag("GroundCheck");
    }
}
