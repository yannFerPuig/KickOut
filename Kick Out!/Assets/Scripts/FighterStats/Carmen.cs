using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class Carmen : FighterStats
{
    void Start()
    {
        maxHealth.SetValue(100);

        width.SetValue(0.3f);
        height.SetValue(0.83f);

        damage.SetValue(10);
        defense.SetValue(10);
        attackRange.SetValue(1.18f);

        moveSpeed.SetValue(5);

        jumpForce.SetValue(30);
        gravityScale.SetValue(10);
        fallingGravityScale.SetValue(40);
        groundCheckRadius.SetValue(0.5f);
    }
}
