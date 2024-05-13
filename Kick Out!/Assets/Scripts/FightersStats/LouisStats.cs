using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class Louis : FighterStats
{
    void Start()
    {
        maxHealth = 100;

        width = 0.3f;
        height = 0.83f;

        damage = 10;
        defense = 10;
        attackRange = 1.18f;

        moveSpeed = 5;

        jumpForce = 30;
        gravityScale = 10;
        fallingGravityScale = 40;
        groundCheckRadius = 0.5f;
    }
}
