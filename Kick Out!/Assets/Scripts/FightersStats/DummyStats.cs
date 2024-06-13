using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class DummyStats : FighterStats
{
    public void Start()
    {
        Name = "Dummy";

        maxHealth = 100;

        width = 0.3f;
        height = 0.8f;

        damage = 0;
        defense = 0;
        attackRange = 0f;

        moveSpeed = 0;

        jumpForce = 0;
        gravityScale = 10;
        fallingGravityScale = 0;
        groundCheckRadius = 0;

        spawnPoint = new Vector3(4.5f, -3.1f, 0f);
        fighterCenter = new Vector3(0, 0, 0);

        currentHealth = maxHealth;
    }
}
