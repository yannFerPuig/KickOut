using UnityEngine;

public class FighterStats : MonoBehaviour
{   
    //COMPONENTS
    public HealthBar healthBar;
    public CapsuleCollider2D capsuleCollider2D;

    //DATA
    public float maxHealth;
    public float currentHealth { get; set;}

    public float width;
    public float height;

    public Stat damage;
    public Stat defense;
    public Stat attackSpeed;
    public Stat attackRange;
    public Stat moveSpeed;
    public Stat jumpForce;
    public Stat gravityScale;
    public Stat fallingGravityScale;
    public Stat blockingSpeed;
    public Stat groundCheckRadius;
    //public string name;


    void Start() 
    {
        currentHealth = maxHealth;
        healthBar.SetMaxHealth(maxHealth);

        capsuleCollider2D.size = new Vector2(width, height); 
    }

    void Awake() 
    {
        currentHealth = maxHealth;
    }
}
