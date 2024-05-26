using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class StartRoundTimer : MonoBehaviour
{
    //TIMERS
    //Texts representing the different timers
    public TextMeshProUGUI startText;
    public TextMeshProUGUI timerText;

    //The starting time of timers (in seconds)
    public float remainingTime = 120;
    public float roundStart = 3;

    public bool fightStarted = false; //To start the fight

    //SCRIPTS
    public MainMenu mainMenu;

    //The players in game
    public GameObject player1;
    public GameObject player2;
    public GameObject iA;

    public void Start()
    {
        //TIMERS
        roundStart = 3;
        remainingTime = 120;

        int minutes = Mathf.FloorToInt(remainingTime / 60); 
        int secondes = Mathf.FloorToInt(remainingTime % 60);

        timerText.text = string.Format("{0:00}:{1:00}", minutes, secondes);

        //Initialize the players so we can disable the scripts
        if (mainMenu.gameMode == "solo")
        {
            player1 = GameObject.FindGameObjectWithTag("Player");
            iA = GameObject.FindGameObjectWithTag("IA");

            player1.GetComponent<PlayerMovement>().enabled = false;
            player1.GetComponent<PlayerAttack>().enabled = false;

            //iA.GetComponent<IAAttack>().enabled = false;
        }
    }

    public void Update()
    {
        if (roundStart > 0)
        {
            roundStart -= Time.deltaTime;
        }
        else if (roundStart < 1) 
        {
            //Disbled the start round coutdown
            startText.gameObject.SetActive(false);

            StartCoroutine(Momentum());

            //Start the fight	
            fightStarted = true;
            //Start the timer for the fight
            RoundTimer();

            
            player1.GetComponent<PlayerMovement>().enabled = true;
            player1.GetComponent<PlayerAttack>().enabled = true;

            //iA.GetComponent<IAAttack>().enabled = true;
        }

        startText.text = string.Format("{0}", Mathf.Round(roundStart));        
    }

    void RoundTimer() 
    {
        if (remainingTime > 0)
        {
            remainingTime -= Time.deltaTime;
        }
        else if (remainingTime < 0) 
        {
            remainingTime = 0;
        }

        int minutes = Mathf.FloorToInt(remainingTime / 60);
        int secondes = Mathf.FloorToInt(remainingTime % 60);

        timerText.text = string.Format("{0:00}:{1:00}", minutes, secondes);
    }

    IEnumerator Momentum() 
    {
        yield return new WaitForSeconds(1f);
    }
}
