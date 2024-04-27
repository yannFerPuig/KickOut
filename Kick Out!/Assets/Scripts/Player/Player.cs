using System.Collections;
using System.Collections.Generic;
using Unity.Netcode;
using UnityEngine;

public class Player : MonoBehaviour
{

    //SCRIPTS
    public FighterStats stats;

    //COMPONENTS
    public HealthBar healthBar;

    //DATA
    public float currentHealth;
    public float defense;

    void Start()
    {
        currentHealth = stats.currentHealth;
        defense = stats.defense.GetValue();
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
