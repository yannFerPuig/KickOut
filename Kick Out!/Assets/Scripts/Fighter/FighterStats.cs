using UnityEngine;

public class FighterStats : MonoBehaviour
{   
    //COMPONENTS
    public HealthBar healthBar;
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
    public float attackRange;
    public float moveSpeed;
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
        healthBar.SetMaxHealth(maxHealth);
    }

    void Awake() 
    {
        currentHealth = maxHealth;
    }
}
