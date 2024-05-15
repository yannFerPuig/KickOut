using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Fighter : MonoBehaviour
{
    //GAMEOBJECTS
    GameObject enemy;

    //SCRIPTS
    public FighterStats stats;
    public MainMenu mainMenu;

    //COMPONENTS
    public HealthBar healthBar;
    public SpriteRenderer spriteRenderer;

    //DATA
    public float currentHealth;
    public float defense;
    public int points;

    void Start()
    {
        mainMenu = GameObject.FindGameObjectWithTag("MainCamera").GetComponent<MainMenu>();
        
        spriteRenderer = GetComponent<SpriteRenderer>();

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

    public void LookAtEnemy()
    {
        if (mainMenu.gameMode == "solo")
        {
            enemy = GameObject.FindGameObjectWithTag("IA");
        }
        else if (mainMenu.gameMode == "duel")
        {
            if (tag == "Player1")
            {
                enemy = GameObject.FindGameObjectWithTag("Player2");
            }
            else 
            {
                enemy = GameObject.FindGameObjectWithTag("Player1");
            }
        }
        
        spriteRenderer.flipX = enemy.transform.position.x < transform.position.x;
    }
}
