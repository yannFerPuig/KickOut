using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EndGame : MonoBehaviour
{
    // END GAME SCREEN
    public Text winnerOfTheMatch;
    
    public void Activate(Fighter winner)
    {
        gameObject.SetActive(true);
        winnerOfTheMatch.text = winner.name + " successfully kicked out his opponent!";
    }
}
