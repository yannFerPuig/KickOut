using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class LouisStats : FighterStats
{
    public void Initialize()
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

        spawnPoint = new Vector3(-8.5f, -2f, 0f);
        groundCheckPointPos = new Vector3(0, -0.3f, 0);
        attackPointPos = new Vector3(0, 0.26f, 0);

        capsuleCollider2D = gameObject.GetComponent<CapsuleCollider2D>();
        attackPoint = GameObject.FindGameObjectWithTag("AttackPoint");
        attackPoint.transform.position = new Vector3(attackPointPos.x, attackPointPos.y, attackPointPos.z);
        groundCheck = GameObject.FindGameObjectWithTag("GroundCheck");

        punchSounds = new List<AudioClip> { Resources.Load<AudioClip>("Sound/Louis/hit-swing-sword-small-2-95566"), Resources.Load<AudioClip>("Sound/Louis/sword-sound-2-36274") };
        specialSounds = new List<AudioClip> { Resources.Load<AudioClip>("Sound/Louis/knife-slice-41231"), Resources.Load<AudioClip>("Sound/Louis/sword-slash-and-swing-185432") };
    }
}
