using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CarmenStats : FighterStats
{
    public void Initialize()
    {
        maxHealth = 100;

        width = 0.3f;
        height = 0.83f;

        damage = 8;
        specialDamage = 15;
        defense = 10;
        attackRange = 1.18f;

        moveSpeed = 5;

        jumpForce = 30f;
        gravityScale = 10f;
        fallingGravityScale = 40f;
        groundCheckRadius = 0.5f;

        spawnPoint = new Vector3(-8f, -2f, 0f);
        groundCheckPointPos = new Vector3(0, -0.3f, 0);
        attackPointPos = new Vector3(0, 0.26f, 0);

        capsuleCollider2D = gameObject.GetComponent<CapsuleCollider2D>();
        attackPoint = GameObject.FindGameObjectWithTag("AttackPoint");
        attackPoint.transform.position = new Vector3(attackPointPos.x, attackPointPos.y, attackPointPos.z);
        groundCheck = GameObject.FindGameObjectWithTag("GroundCheck");

        punchSounds = new List<AudioClip> { Resources.Load<AudioClip>("Sound/Carmen/punch-7-166700"), Resources.Load<AudioClip>("Sound/Carmen/punch-1-166694"), Resources.Load<AudioClip>("Assets/Sound/Carmen/punch-2-166695") };
        specialSounds = new List<AudioClip> { )Resources.Load<AudioClip>("Sound/Carmen/Special-punch1"), Resources.Load<AudioClip>("Sound/Carmen/Special-punch2") };
    }
}
