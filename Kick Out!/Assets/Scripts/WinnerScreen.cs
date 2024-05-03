using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;



public class WinnerScreen : MonoBehaviour
{// DISPLAYS THE ROUND WINNER



    public EndGame endGame;
    public Text roundWinner;
    public Player player1;
    public List<Image> player1Points;
    public IA player2;
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

    public void Setup(Player winner)
    {if (winner.points < 3)
        {
            gameObject.SetActive(true);
            roundWinner.text = winner.name;
            player1Points[player1.points - 1].GetComponent<Image>().color = new Color32(0, 255, 0, 255);

            player1.currentHealth = 100;
            player2.currentHealth = 100;
            
        }
        else
        { endGame.Activate(winner); }
    }
    public void Setup(IA winner)
    {
        if (winner.points < 3)
        {
            gameObject.SetActive(true);
            roundWinner.text = winner.name;
            player2Points[player1.points - 1].GetComponent<Image>().color = new Color32(0, 255, 0, 255);

            player1.currentHealth = 100;
            player2.currentHealth = 100;
            
        }
        else
        { endGame.Activate(winner); }

    }
}



