using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class LouisStats : FighterStats
{
    public void Initialize()
    {
        Name = "Louis";

        maxHealth = 100;

        width = 0.3f;
        height = 1f;
        crouchWidth = 0.3f;  
        crouchHeight = 0.55f;

        offsetX = 0;
        offsetY = 0;
        crouchOffsetX = -0.1f;
        crouchOffsetY = -0.28f; 

        damage = 10;
        defense = 10;
        attackRange = 4.8f;

        moveSpeed = 5;

        jumpForce = 30;
        gravityScale = 10;
        fallingGravityScale = 40;
        groundCheckRadius = 0.5f;

        spawnPoint = new Vector3(-8.5f, -2f, 0f);
        groundCheckPointPos = new Vector3(0f, -0.51f, 0);
        attackPointPos = new Vector3(0, 0.15f, 0);

        capsuleCollider2D = gameObject.GetComponent<CapsuleCollider2D>();
        
        attackPoint = GameObject.FindGameObjectWithTag("AttackPoint");
        attackPoint.transform.position = new Vector3(attackPointPos.x, attackPointPos.y, attackPointPos.z);
        
        groundCheck = GameObject.FindGameObjectWithTag("GroundCheck");
        attackPoint.transform.position = new Vector3(groundCheckPointPos.x, groundCheckPointPos.y, groundCheckPointPos.z);
    }
}
