using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Fighter : MonoBehaviour
{
    //SCRIPTS
    public FighterStats stats;
    public MainMenu mainMenu;

    //COMPONENTS
    public HealthBar healthBar;
    public SpriteRenderer spriteRenderer;

    //GAMEOBJECTS
    public GameObject enemy;

    //DATA
    public float currentHealth;
    public float defense;
    public int points;

    void Start()
    {
        stats = gameObject.GetComponent<FighterStats>();
        mainMenu = GameObject.Find("Main Camera").GetComponent<MainMenu>();

        spriteRenderer = gameObject.GetComponent<SpriteRenderer>();

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
            enemy = GameObject.Find("IA");
            LookAt(enemy);
        }
    }

    void LookAt(GameObject enemy)
    {
        if (transform.position.x > enemy.transform.position.x)
        {
            spriteRenderer.flipX = true;
        }
        else
        {
            spriteRenderer.flipX = false;
        }
    }
}
