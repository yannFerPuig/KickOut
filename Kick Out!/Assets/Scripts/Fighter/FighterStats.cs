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
    public Vector3 spawnPoint;
    public Vector3 attackPointPos;
    public Vector3 groundCheckPointPos;

    void Start() 
    {
        fighter = gameObject.GetComponent<Fighter>();
        
        fighter.healthBar.SetMaxHealth(maxHealth);

        currentHealth = maxHealth;
        
        if (gameObject.tag == "Player")
        {
            blockSlider = GameObject.Find("BlockP1").GetComponent<Slider>();
        }
        else if (gameObject.tag == "AI")
        {
            blockSlider = GameObject.Find("BlockP2").GetComponent<Slider>();
        }

    }

    void Awake() 
    {
        currentHealth = maxHealth;
    }
}
