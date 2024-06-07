using UnityEngine.UI;
using UnityEngine;

public class Fighter : MonoBehaviour
{
    //SCRIPTS
    public FighterStats stats;
    public FighterStats enemyStats;
    public MainMenu mainMenu;

    //COMPONENTS
    public HealthBar healthBar;
    public SpriteRenderer spriteRenderer;

    //GAMEOBJECT
    public GameObject enemy;
    public GameObject imageRound1;
    public GameObject imageRound2;
    public GameObject imageRound3;

    //DATA
    public float currentHealth;
    public int points;

    void Start()
    {
        mainMenu = GameObject.Find("Main Camera").GetComponent<MainMenu>();

        //To define who is the enemy of the current fighter
        if (mainMenu.gameMode == "solo")
        {
            enemy = GameObject.Find("IA");

            if (gameObject.CompareTag("Player"))
            {
                imageRound1 = GameObject.Find("P1R1");
                imageRound2 = GameObject.Find("P1R2");
                imageRound3 = GameObject.Find("P1R3");    
            }
            else 
            {
                imageRound1 = GameObject.Find("P2R1");
                imageRound2 = GameObject.Find("P2R2");
                imageRound3 = GameObject.Find("P2R3");    
            }
        }
        else 
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

        currentHealth = stats.currentHealth;
        points = 0;        
    }

    public void TakeDamage(float damage)
    {
        currentHealth -= damage * (1 - stats.defense/100);

        healthBar.SetHealth(currentHealth);

        if (currentHealth <= 0)
            Die();
    }

    public virtual void Die() 
    { 
        //if the fighter dies, we add 1 to the rounds won by the enemy
        enemyStats.roundsWon++;

        //we color the square representing that the round has been won
        switch (enemyStats.roundsWon)
        {
            case 1:
                imageRound1.GetComponent<Image>().color = Color.green;
            break;  

            case 2:
                imageRound2.GetComponent<Image>().color = Color.green;
            break;
        
            case 3:
                imageRound3.GetComponent<Image>().color = Color.green;
            break;

            default:
            break;
        }
    }

    public void LookAtEnemy()
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

    public void GameOver()
    {
        if (mainMenu.gameMode == "solo")
        {

        }
        else
        {

        }
    }
}
