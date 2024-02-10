using UnityEngine;

public class FighterStats : MonoBehaviour
{
    public float maxHealth;
    public float currentHealth { get; private set;}

    public Stat damage;
    public Stat defense;
    public Stat attackSpeed;
    public Stat moveSpeed;
    public Stat blockingSpeed;

    void Awake() 
    {
        currentHealth = maxHealth;
    }

    public void TakeDamage(int damage)
    {
        currentHealth -= damage * (1 - defense.GetValue()/100);

        if (currentHealth <= 0)
            Die();
    }

    public virtual void Die() 
    {
        Debug.Log(transform.name + "is dead");
    }
}
