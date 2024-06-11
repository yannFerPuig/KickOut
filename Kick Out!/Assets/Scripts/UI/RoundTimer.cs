using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;
using Unity.VisualScripting.Dependencies.Sqlite;
using UnityEngine.UIElements;

public class RoundTimer : MonoBehaviour
{
    //SCRIPTS
    public MainMenu mainMenu;
    public RoundManager roundManager;

    //The starting time of timers (in seconds)
    public float remainingTime = 120;
    public float roundStart = 3;

    public bool fightStarted = false;


    //GAMEOBJECTS
    public GameObject player1;
    public GameObject player2;
    public GameObject iA;

    public TextMeshProUGUI startText;
    public TextMeshProUGUI timerText;


    public void Start()
    {
        mainMenu = GameObject.FindGameObjectWithTag("MainCamera").GetComponent<MainMenu>(); 
        roundManager = gameObject.GetComponent<RoundManager>();

        startText = GameObject.Find("Start Timer").GetComponent<TextMeshProUGUI>();
        timerText = GameObject.Find("Round Timer").GetComponent<TextMeshProUGUI>();

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
            player2 = GameObject.FindGameObjectWithTag("AI");

            player1.GetComponent<PlayerMovement>().enabled = false;
            player1.GetComponent<PlayerAttack>().enabled = false;

            player2.GetComponent<AIMovement>().enabled = false;
            player2.GetComponent<AIAttack>().enabled = false;
        }
    }

    public void Update()
    {
        if (!fightStarted)
        {
            timerText.text = string.Format("{0:00}:{1:00}", 2, 0);
        }
        else 
        {
            int minutes = Mathf.FloorToInt(remainingTime / 60); 
            int secondes = Mathf.FloorToInt(remainingTime % 60);

            timerText.text = string.Format("{0:00}:{1:00}", minutes, secondes);
        }

        if (roundStart > 0)
        {
            roundStart -= Time.deltaTime;
        }
        else if (roundStart < 1) 
        {
            StartRound();
        }

        startText.text = string.Format("{0}", Mathf.Round(roundStart));        
    }

    void StartRound()
    {
        startText.gameObject.SetActive(false);

        StartCoroutine(Momentum());

        fightStarted = true;

        Timer();

        if (mainMenu.gameMode == "solo")
        {
            player1.GetComponent<PlayerMovement>().enabled = true;
            player1.GetComponent<PlayerAttack>().enabled = true;

            player2.GetComponent<AIMovement>().enabled = true;
            player2.GetComponent<AIAttack>().enabled = true;
        }
    }

    void Timer() 
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

    public void ResetTimer()
    {
        if (player1.GetComponent<PlayerMovement>().enabled)
        {
            player1.GetComponent<PlayerMovement>().enabled = false;
        }

        if (player1.GetComponent<PlayerAttack>().enabled)
        {    
            player1.GetComponent<PlayerAttack>().enabled = false;
        }

        if (mainMenu.gameMode == "solo" && player2.GetComponent<AIMovement>().enabled)
        {
            player2.GetComponent<AIMovement>().enabled = false;
        }

        if (mainMenu.gameMode == "solo" && player2.GetComponent<AIAttack>().enabled)
        {    
            player2.GetComponent<AIAttack>().enabled = false;
        }


        remainingTime = 120f;
        roundStart = 3f;
        fightStarted = false;

        startText.gameObject.SetActive(true);
    }

    IEnumerator Momentum() 
    {
        yield return new WaitForSeconds(1f);
    }
}
