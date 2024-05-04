using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class StartRoundTimer : MonoBehaviour
{
    //#####################################################################################################
    //TIMERS
    //Texts representing the different timers
    public TextMeshProUGUI startText;
    public TextMeshProUGUI timerText;

    //The starting time of timers (in seconds)
    float remainingTime = 120;
    float roundStart = 3;

    //To start the fight
    public bool fightStarted = false;

    public void Start()
    {
        //TIMERS
        roundStart = 3;

        int minutes = Mathf.FloorToInt(remainingTime / 60); 
        int secondes = Mathf.FloorToInt(remainingTime % 60);

        timerText.text = string.Format("{0:00}:{1:00}", minutes, secondes);
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
