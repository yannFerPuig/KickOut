using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class FighterStats : MonoBehaviour
{   
    //GAMEOBJECTS
    public HealthBar healthBar;
    public Slider blockSlider;
    public GameObject attackPoint;
    public GameObject groundCheck;


    //COMPONENTS
    public CapsuleCollider2D capsuleCollider2D;
    //STAT DATA

    //Health
    public float maxHealth;
    public float currentHealth;

    //Size
    public float width;
    public float height;

    //Attack
    public float damage;
    public float specialDamage;
    public float attackRange;
    public float attackSpeed;

    //Defense
    public float defense;
    public float blockRadius = 5f;
    public float blockCD = 3.5f;

    //Block cooldowns
    public float reduceCD;
    public float specialReduceCD;
    public float moveSpeed;
    public float blockingMoveSpeed;
    
    //Jump
    public float jumpForce;
    public float gravityScale;
    public float fallingGravityScale;
    public float groundCheckRadius;

    //Rounds
    public float roundsWon = 0;

    //ADDITIONNAL DATA
    public Vector3 spawnPoint;
    public Vector3 attackPointPos;
    public Vector3 groundCheckPointPos;
    public List<AudioClip> punchSounds;
    public List<AudioClip> specialSounds;

    void Start() 
    {
        currentHealth = maxHealth;
        //healthBar.SetMaxHealth(maxHealth);
        blockingMoveSpeed = moveSpeed / 2;

        if (gameObject.CompareTag("Player") || gameObject.CompareTag("Player1"))
        {
            blockSlider = GameObject.Find("BlockP1").GetComponent<Slider>();
        }
        else 
        {
            //cdBlock = GameObject.Find("BlockP2");
        }

    }

    void Awake() 
    {
        currentHealth = maxHealth;
    }
}
