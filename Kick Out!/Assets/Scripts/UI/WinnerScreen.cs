using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class WinnerScreen : MonoBehaviour
{
    // DISPLAYS THE ROUND WINNER
    public EndGame endGame;
    public StartRoundTimer startRoundTimer;
    public TextMeshProUGUI roundWinner;
    public Fighter player1;
    public Fighter player2;
    public List<Image> player1Points;
    public List<Image> player2Points;

    // Start is called before the first frame update
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
        if (player1.currentHealth <= 0 && player2.currentHealth <= 0)
            Setup(player1);
        else if (player1.currentHealth <= 0)
        {
            player2.points++;
            Setup(player2);
        }
        else if (player2.currentHealth <= 0)
        {
            player1.points++;
            Setup(player1);
        }
    }

    public void Setup(Fighter winner)
    {
        //if the winner of the round hasn't already won 3 rounds we launch another round 
        if (winner.points < 3) 
        {
            gameObject.SetActive(true);
            roundWinner.text = winner.name;

            //if the winner is the P1
            if (winner.tag == "P1")
            {
                player1Points[player1.points - 1].GetComponent<Image>().color = Color.green;
            }
            else if (winner.tag == "P2")
            {
                player2Points[player2.points - 1].GetComponent<Image>().color = Color.green;
            }

            //we reset the health of the fighters
            player1.currentHealth = 100;
            player2.currentHealth = 100;

            //we also reset the visual of the healthbar
            GameObject.FindGameObjectWithTag("HP_P1").GetComponent<HealthBar>().SetHealth(player1.currentHealth);
            GameObject.FindGameObjectWithTag("HP_P2").GetComponent<HealthBar>().SetHealth(player1.currentHealth);

            //we make the fighters respawn at their original location
            player1.transform.position = new Vector3(-8.5f, -2f, 0f);
            player2.transform.position = new Vector3(8.5f, -2.43f, 0f);
        }
        else 
        {
            endGame.Activate(winner);
        }

    }
}



