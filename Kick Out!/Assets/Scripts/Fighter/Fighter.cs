using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Fighter : MonoBehaviour
{
    //SCRIPTS
    public FighterStats stats;

    //COMPONENTS
    public HealthBar healthBar;

    //DATA
    public float currentHealth;
    public float defense;
    public int points;

    void Start()
    {
        stats = gameObject.GetComponent<FighterStats>();

        currentHealth = stats.currentHealth;
        defense = stats.defense;
        points = 0;
    }

    public void TakeDamage(float damage)
    {
        currentHealth -= damage * (1 - defense/100);

        healthBar.SetHealth(currentHealth);

        if (currentHealth <= 0)
            Die();
    }

    public virtual void Die() 
    {
        Debug.Log(transform.name + " is dead");
    }
}
