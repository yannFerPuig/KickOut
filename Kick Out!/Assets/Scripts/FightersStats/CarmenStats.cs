using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class CarmenStats : FighterStats
{
    public void Initialize()
    {
        Name = "Carmen";

        maxHealth = 100;

        width = 0.3f;
        height = 0.8f;
        crouchWidth = 0.3f;  
        crouchHeight = 0.5f;

        offsetX = 0;
        offsetY = 0;
        crouchOffsetX = 0f;
        crouchOffsetY = -0.15f; 

        damage = 8;
        specialDamage = 15;
        defense = 10;
        attackRange = 2f;
        reduceCD = 0.08f;

        moveSpeed = 5;

        jumpForce = 35f;
        gravityScale = 10f;
        fallingGravityScale = 40f;
        groundCheckRadius = 0.5f;

        spawnPoint = new Vector3(-8f, -2f, 0f);
        groundCheckPointPos = new Vector3(0, -0.36f, 0);
        attackPointPos = new Vector3(0, 0.26f, 0);
        fighterCenter = new Vector3(0, 0, 0);
    }
}
