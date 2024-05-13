using UnityEngine;

public class FighterStats : MonoBehaviour
{   
    //COMPONENTS
    public HealthBar healthBar;
    public CapsuleCollider2D capsuleCollider2D;

    //DATA
    public Stat maxHealth;
    public Stat currentHealth;
    public Stat width;
    public Stat height;
    public Stat damage;
    public Stat defense;
    public Stat attackSpeed;
    public Stat attackRange;
    public Stat moveSpeed;
    public Stat jumpForce;
    public Stat gravityScale;
    public Stat fallingGravityScale;
    public Stat groundCheckRadius;
    //public string name;


    void Start() 
    {
        currentHealth = maxHealth;
        healthBar.SetMaxHealth(maxHealth.GetValue());

        capsuleCollider2D.size = new Vector2(width.GetValue(), height.GetValue()); 
    }

    void Awake() 
    {
        currentHealth = maxHealth;
    }
}
