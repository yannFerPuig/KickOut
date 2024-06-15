using System.Collections;
using System.Collections.Generic;
using JetBrains.Annotations;
using UnityEngine;
using UnityEngine.UIElements;

public class LouisStats : FighterStats
{
    public float flippedOffsetX = 0.2f;
    public float flippedCrouchOffsetX = 0.1f;

    public void Initialize()
    {
        Name = "Louis";
        
        punchSound = Resources.Load<AudioClip>("Sound/Louis/louisPunch");
        specialSound = Resources.Load<AudioClip>("Sound/Louis/louisSpecial");
        missShot = Resources.Load<AudioClip>("Sound/missedShot");

        maxHealth = 180;

        width = 0.3f;
        height = 1f;
        crouchWidth = 0.3f;  
        crouchHeight = 0.58f;

        offsetX = -0.2f;
        offsetY = 0;
        crouchOffsetX = -0.1f;
        crouchOffsetY = -0.24f; 

        damage = 10;
        specialDamage = 50f;
        defense = 10;
        attackRange = 5f;
        attackRangeSpe = 5f;
        attackCooldown = 1.5f;
        attackCooldownSpe = 10f;
        reduceCD = 1.3f;

        moveSpeed = 5;

        jumpForce = 30;
        gravityScale = 10;
        fallingGravityScale = 40;
        groundCheckRadius = 0.5f;

        spawnPoint = new Vector3(-6.5f, -2f, 0f);
        groundCheckPointPos = new Vector3(0.3f, -0.5f, 0);
        attackPointPos = new Vector3(0, 0.15f, 0);
        fighterCenter = new Vector3(-0.25f, 0, 0);

        punchSounds = new List<AudioClip> { Resources.Load<AudioClip>("Sound/Louis/hit-swing-sword-small-2-95566"), Resources.Load<AudioClip>("Sound/Louis/sword-sound-2-36274") };
        specialSounds = new List<AudioClip> { Resources.Load<AudioClip>("Sound/Louis/knife-slice-41231"), Resources.Load<AudioClip>("Sound/Louis/sword-slash-and-swing-185432") };
    }
}
