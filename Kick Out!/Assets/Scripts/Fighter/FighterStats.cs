using UnityEngine;
using UnityEngine.UI;

public class FighterStats : MonoBehaviour
{   
    //GAMEOBJECTS
    public HealthBar healthBar;
    public GameObject cdBlock;

    //COMPONENTS
    public CapsuleCollider2D capsuleCollider2D;
    public GameObject attackPoint;
    public GameObject groundCheck;

    //STAT DATA
    public float maxHealth;
    public float currentHealth;

    public float width;
    public float height;
    
    public float damage;
    public float specialDamage;
    
    public float defense;
    public float blockRadius = 5f;
    public float blockCD = 2.5f ;

    public float attackRange;
    public float attackSpeed;

    public float moveSpeed;
    public float blockingMoveSpeed;
    
    public float jumpForce;
    public float gravityScale;
    public float fallingGravityScale;
    public float groundCheckRadius;

    //ADDITIONNAL DATA
    public Vector3 spawnPoint;
    public Vector3 attackPointPos;
    public Vector3 groundCheckPointPos;


    void Start() 
    {
        currentHealth = maxHealth;
        //healthBar.SetMaxHealth(maxHealth);
        blockingMoveSpeed = moveSpeed / 2;
    }

    void Awake() 
    {
        currentHealth = maxHealth;
    }
}
