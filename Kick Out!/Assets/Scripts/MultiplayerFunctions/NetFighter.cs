using UnityEngine;
using Unity.Netcode;
using UnityEngine.UI;
using System;

public class NetFighter : NetworkBehaviour
{
    // SCRIPTS
    public FighterStats stats;
    public FighterStats enemyStats;
    public MainMenu mainMenu;

    // COMPONENTS
    public HealthBar healthBar;
    public SpriteRenderer spriteRenderer;

    // GAMEOBJECT
    public GameObject enemy;
    public GameObject imageRound1;
    public GameObject imageRound2;
    public GameObject imageRound3;

    // DATA
    public int points;

    void Start()
    {
        if (IsServer)
        {
            mainMenu = GameObject.Find("Main Camera").GetComponent<MainMenu>();

            if (mainMenu.gameMode == "solo")
            {
                if (gameObject.tag == "Player")
                {
                    enemy = GameObject.FindGameObjectWithTag("AI");

                    imageRound1 = GameObject.Find("P1R1");
                    imageRound2 = GameObject.Find("P1R2");
                    imageRound3 = GameObject.Find("P1R3");

                    healthBar = GameObject.FindGameObjectWithTag("HP_P1").GetComponent<HealthBar>();
                }
                else if (gameObject.tag == "AI")
                {
                    enemy = GameObject.FindGameObjectWithTag("Player");

                    imageRound1 = GameObject.Find("P2R1");
                    imageRound2 = GameObject.Find("P2R2");
                    imageRound3 = GameObject.Find("P2R3");

                    healthBar = GameObject.FindGameObjectWithTag("HP_P2").GetComponent<HealthBar>();
                }
            }
            else if (mainMenu.gameMode == "tutorial")
            {
                if (gameObject.tag == "Player")
                {
                    enemy = GameObject.FindGameObjectWithTag("Dummy");

                    imageRound1 = GameObject.Find("P1R1");
                    imageRound2 = GameObject.Find("P1R2");
                    imageRound3 = GameObject.Find("P1R3");

                    healthBar = GameObject.FindGameObjectWithTag("HP_P1").GetComponent<HealthBar>();
                }
                else if (gameObject.tag == "Dummy")
                {
                    enemy = GameObject.FindGameObjectWithTag("Player");

                    healthBar = GameObject.FindGameObjectWithTag("HP_P2").GetComponent<HealthBar>();
                }
            }
            else if (mainMenu.gameMode == "duel")
            {
                if (gameObject.CompareTag("Player1"))
                {
                    enemy = GameObject.Find("Player2");

                    imageRound1 = GameObject.Find("P1R1");
                    imageRound2 = GameObject.Find("P1R2");
                    imageRound3 = GameObject.Find("P1R3");
                }
                else
                {
                    enemy = GameObject.FindGameObjectWithTag("Player1");

                    imageRound1 = GameObject.Find("P2R1");
                    imageRound2 = GameObject.Find("P2R2");
                    imageRound3 = GameObject.Find("P2R3");
                }
            }

            stats = gameObject.GetComponent<FighterStats>();
            enemyStats = enemy.GetComponent<FighterStats>();

            spriteRenderer = gameObject.GetComponent<SpriteRenderer>();

            stats.currentHealth = stats.maxHealth;
            points = 0;
        }
    }

    [ServerRpc]
    public void TakeDamageServerRpc(float damage)
    {
        TakeDamage(damage);
        TakeDamageClientRpc(damage);
    }

    [ClientRpc]
    private void TakeDamageClientRpc(float damage)
    {
        if (!IsServer)
        {
            TakeDamage(damage);
        }
    }

    private void TakeDamage(float damage)
    {
        stats.currentHealth -= damage * (1 - stats.defense / 100);
        healthBar.SetHealth(stats.currentHealth);
    }

    [ServerRpc]
    public void LookAtEnemyServerRpc()
    {
        LookAtEnemy();
        LookAtEnemyClientRpc();
    }

    [ClientRpc]
    private void LookAtEnemyClientRpc()
    {
        if (!IsServer)
        {
            LookAtEnemy();
        }
    }

    private void LookAtEnemy()
    {
        if (stats.center.transform.position.x > enemy.GetComponent<FighterStats>().center.transform.position.x)
        {
            if (stats is LouisStats l)
            {
                l.capsuleCollider2D.offset = new Vector2(l.flippedOffsetX, l.offsetY);
                l.center.transform.position = new Vector3(gameObject.transform.position.x - l.fighterCenter.x * 5, gameObject.transform.position.y + l.fighterCenter.y * 5, gameObject.transform.position.z + l.fighterCenter.z);
            }

            spriteRenderer.flipX = true;
        }
        else
        {
            if (stats is LouisStats l)
            {
                l.capsuleCollider2D.offset = new Vector2(l.offsetX, l.offsetY);
                l.center.transform.position = new Vector3(gameObject.transform.position.x + l.fighterCenter.x * 5, gameObject.transform.position.y + l.fighterCenter.y * 5, gameObject.transform.position.z + l.fighterCenter.z);
            }

            spriteRenderer.flipX = false;
        }
    }
}
