using UnityEngine;
using UnityEngine.UI;

public class RoundManager : MonoBehaviour
{
    //SCRIPTS
    public StartRoundTimer roundTimer;

    //GAMEOBJECTS
    public Fighter player1;
    public Fighter player2;

    //DATA
    public string roundWinnerName;

    void Start()
    {
        roundTimer = GameObject.Find("Canvas").GetComponent<StartRoundTimer>();        
    }

    void Update()
    {
        if (roundTimer.remainingTime == 0 && player1.currentHealth > 0 && player2.currentHealth > 0)
        {
            VictoryByTime();
        }
    }

    void VictoryByTime()
    {
        if (player1.currentHealth > player2.currentHealth)
        {
            SetPlayerVictory(player1);
        }
        else
        {
            SetPlayerVictory(player2);
        }
    }

    void Victory()
    {
        if (player1.currentHealth <= 0)
        {
            SetPlayerVictory(player2);
        }
        else 
        {
            SetPlayerVictory(player1);
        }
    }

    void SetPlayerVictory(Fighter player) 
    {
        roundWinnerName = player2.name;

        player.points++;
        
        switch(player.points)
        {
            case 1:
                player.imageRound1.GetComponent<Image>().color = Color.green;
                break;
            case 2:
                player.imageRound3.GetComponent<Image>().color = Color.green;
                break;
            case 3:
                player.imageRound3.GetComponent<Image>().color = Color.green;
                break;
        }
    }
}
