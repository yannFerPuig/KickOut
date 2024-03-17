using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NewBehaviourScript : MonoBehaviour
{
    
    public FighterStats stats;

    public float maxHealth;
    public float currentHealth;


    // Start is called before the first frame update
    void Start()
    {
        maxHealth = stats.maxHealth;
        currentHealth = maxHealth;
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.O))
        {
            stats.TakeDamage(10);
        }   
    }
}
