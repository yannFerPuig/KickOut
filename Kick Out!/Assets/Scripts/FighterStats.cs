using UnityEngine;

public class FighterStats : MonoBehaviour
{   
    public HealthBar healthBar;

    public CapsuleCollider2D capsuleCollider2D;

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
    public void TakeDamage(float damage)
    {
        currentHealth -= damage * (1 - defense.GetValue()/100);

        healthBar.SetHealth(currentHealth);

        if (currentHealth <= 0)
            Die();
    }

    public virtual void Die() 
    {
        Debug.Log(transform.name + " is dead");
    }
}
