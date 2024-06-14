using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class BobUnStats : FighterStats
{
    public void Initialize()
    {
        Name = "Bob Un";

        maxHealth = 140;

        width = 0.5f;
        height = 0.83f;
        crouchWidth = 0.6f;  
        crouchHeight = 0.63f;

        offsetX = -0.05f;
        offsetY = 0;
        crouchOffsetX = -0.01f;
        crouchOffsetY = -0.1f; 

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
        fighterCenter = new Vector3(0, 0, 0);
    }
}
