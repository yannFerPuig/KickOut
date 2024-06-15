using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class FighterStats : MonoBehaviour
{   
    //SCRIPTS
    public HealthBar healthBar;
    public Fighter fighter;

    //GAMEOBJECTS
    public Slider blockSlider;
    public GameObject attackPoint;
    public GameObject groundCheck;
    public GameObject center;

    //COMPONENTS
    public CapsuleCollider2D capsuleCollider2D;

    //STAT DATA

    //Name
    public string Name;

    //Health
    public float maxHealth;
    public float currentHealth;

    //Size
    public float width;
    public float height;
    public float crouchWidth;
    public float crouchHeight;

    //Offset
    public float offsetX;
    public float offsetY;
    public float crouchOffsetX;
    public float crouchOffsetY;

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
    public Vector3 fighterCenter;
    public Vector3 spawnPoint;
    public Vector3 attackPointPos;
    public Vector3 groundCheckPointPos;


    public AudioClip punchSound;
    public AudioClip specialSound;
    public AudioClip missShot;

    public List<AudioClip> punchSounds;
    public List<AudioClip> specialSounds;


    void Start() 
    {
        fighter = gameObject.GetComponent<Fighter>();
        
        fighter.healthBar.SetMaxHealth(maxHealth);

        currentHealth = maxHealth;
        
        if (gameObject.tag == "Player" || gameObject.tag  == "Player1")
        {
            blockSlider = GameObject.Find("BlockP1").GetComponent<Slider>();
        }
        else if (gameObject.tag == "AI" || gameObject.tag  == "Player2")
        {
            blockSlider = GameObject.Find("BlockP2").GetComponent<Slider>();
        }


    }

    void Awake() 
    {
        currentHealth = maxHealth;

        capsuleCollider2D = gameObject.GetComponent<CapsuleCollider2D>();

        if (gameObject.tag == "Player" || gameObject.tag == "Player1")
        {    
            attackPoint = GameObject.FindGameObjectWithTag("AttackPoint1");
            groundCheck = GameObject.FindGameObjectWithTag("GroundCheck1");
            center = GameObject.FindGameObjectWithTag("Center1");
        }
        else if (gameObject.tag == "AI" || gameObject.tag == "Player2")
        {
            attackPoint = GameObject.FindGameObjectWithTag("AttackPoint2");
            groundCheck = GameObject.FindGameObjectWithTag("GroundCheck2");
            center = GameObject.FindGameObjectWithTag("Center2");
        }
    }
}
