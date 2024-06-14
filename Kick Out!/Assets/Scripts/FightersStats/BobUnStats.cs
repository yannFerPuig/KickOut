using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class BobUnStats : FighterStats
{
    public void Initialize()
    {
        Name = "Bob Un";

        punchSound = Resources.Load<AudioClip>("Sound/BobUn/bobUnPunch");
        punchSound = Resources.Load<AudioClip>("Sound/BobUn/bobUnSpecial");

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


        punchSounds = new List<AudioClip> { Resources.Load<AudioClip>("Sound/BobUn/hard-punch-80578"), Resources.Load<AudioClip>("Sound/BobUn/086081_punch-5wav-36765"), Resources.Load<AudioClip>("Sound/BobUn/086084_punch-1wav-36766") };
        specialSounds = new List<AudioClip> { Resources.Load<AudioClip>("fast-simple-chop-5-6270"), Resources.Load<AudioClip>("Sound/BobUn/fast-simple-chop-6"), Resources.Load<AudioClip>("Sound/BobUn/fast-simple-chop-7"), Resources.Load<AudioClip>("Sound/BobUn/fast-simple-chop-9") };

    }
}
