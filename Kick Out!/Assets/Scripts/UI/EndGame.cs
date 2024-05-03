using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EndGame : MonoBehaviour
{// END GAME SCREEN
    public Text winnerOfTheMatch;
    public void Activate(Player winner)
    {
        gameObject.SetActive(true);
        winnerOfTheMatch.text = winner.name + " successfully kicked out his opponent!";
    }
    public void Activate(IA winner)
    {
        gameObject.SetActive(true);
        winnerOfTheMatch.text = winner.name + " successfully kicked out his opponent!";
    }
}
