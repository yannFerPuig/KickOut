using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class RoundManager : MonoBehaviour
{
    //SCRIPTS
    public RoundTimer roundTimer;
    public MainMenu menu;

    //GAMEOBJECTS
    public Fighter player1;
    public Fighter player2;

    //DATA
    public string roundWinner;
    public string fightWinner;

    public int roundNumber = 0;

    void Start()
    {
        roundTimer = gameObject.GetComponent<RoundTimer>();        
        menu = gameObject.GetComponent<MainMenu>();

        if (menu.gameMode == "tutorial")
        {
            player1 = GameObject.FindGameObjectWithTag("Player").GetComponent<Fighter>();
            player2 = GameObject.FindGameObjectWithTag("Dummy").GetComponent<Fighter>();
        }
        else if (menu.gameMode == "solo")
        {
            player1 = GameObject.FindGameObjectWithTag("Player").GetComponent<Fighter>();
            player2 = GameObject.FindGameObjectWithTag("AI").GetComponent<Fighter>();
        }
        else if (menu.gameMode == "duel")
        {

        }
    }

    void Update()
    {
        if (menu.gameMode != "tutorial" && roundTimer.remainingTime == 0 && player1.currentHealth > 0 && player2.currentHealth > 0)
        {
            VictoryByTime();
        }

        Victory();
        FightVictory();
    }

    void VictoryByTime()
    {
        if (player1.currentHealth > player2.currentHealth)
        {
            SetPlayerVictory(player1);
            ResetRound();
            roundTimer.ResetTimer();
            roundNumber++;
        }
        else
        {
            SetPlayerVictory(player2);
            ResetRound();
            roundTimer.ResetTimer();
            roundNumber++;
        }
    }

    void Victory()
    {
        if (player1.currentHealth <= 0)
        {
            SetPlayerVictory(player2);
            ResetRound();
            roundTimer.ResetTimer();
            roundNumber++;
        }
        else if (player2.currentHealth <= 0)
        {
            SetPlayerVictory(player1);
            ResetRound();
            roundTimer.ResetTimer();
            roundNumber++;
        }
    }

    void SetPlayerVictory(Fighter player) 
    {
        roundWinner = player.name;

        player.points++;
        
        switch(player.points)
        {
            case 1:
                player.imageRound1.GetComponent<Image>().color = Color.yellow;
                break;
            case 2:
                player.imageRound2.GetComponent<Image>().color = Color.yellow;
                break;
            case 3:
                player.imageRound3.GetComponent<Image>().color = Color.yellow;
                break;
        }
    }

    void ResetRound()
    {
        player1.transform.position = new Vector3(player1.stats.spawnPoint.x, player1.stats.spawnPoint.y, player1.stats.spawnPoint.z);
        player2.transform.position = new Vector3(player2.stats.spawnPoint.x, player2.stats.spawnPoint.y, player2.stats.spawnPoint.z);

        player1.currentHealth = player1.stats.maxHealth;
        player2.currentHealth = player2.stats.maxHealth;

        player1.healthBar.SetMaxHealth(player1.stats.maxHealth);
        player2.healthBar.SetMaxHealth(player2.stats.maxHealth);
    }

    void FightVictory()
    {
        if (player1.points == 3)
        {
            fightWinner = player1.stats.Name.ToUpper();
            StartCoroutine(Momentum());
            menu.EndFight();
        }
        else if (player2.points == 3)
        {
            fightWinner = player2.stats.Name.ToUpper();
            StartCoroutine(Momentum());
            menu.EndFight();
        }
    }

    IEnumerator Momentum()
    {
        yield return new WaitForSeconds(1f);
    }
}
