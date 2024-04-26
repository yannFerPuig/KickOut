using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class CountdownTmer : MonoBehaviour
{
    public TextMeshPro countdownTimer;

    float currentTime;
    public float startingTime; 

    void Start()
    {
        countdownTimer = GetComponent<TextMeshPro>();
        currentTime = startingTime;
    }

    void Update()
    {
        currentTime -= 1 * Time.deltaTime;
    }
}
