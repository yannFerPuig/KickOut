using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Fight : MonoBehaviour
{
    //SCRIPTS
    public MainMenu menu;

    //GAMEOBJECTS
    public Image fighter1Portrait;
    public Image fighter2Portrait;

    public GameObject fighter1;
    public GameObject fighter2;

    void Start()
    {
        menu = GameObject.FindGameObjectWithTag("MainCamera").GetComponent<MainMenu>();

        fighter1Portrait = GameObject.FindGameObjectWithTag("Image1").GetComponent<Image>();
        fighter2Portrait = GameObject.FindGameObjectWithTag("Image2").GetComponent<Image>();

        if (menu.gameMode == "tutorial")
        {
            fighter1 = GameObject.FindGameObjectWithTag("Player");
            fighter2 = GameObject.FindGameObjectWithTag("Dummy");

            fighter1Portrait.sprite = Resources.Load<Sprite>($"Portraits/{fighter1.GetComponent<FighterStats>().Name}");
            fighter2Portrait.sprite = Resources.Load<Sprite>("Portraits/Dummy");
        }
        else if (menu.gameMode == "solo")
        {
            fighter1 = GameObject.FindGameObjectWithTag("Player");
            fighter2 = GameObject.FindGameObjectWithTag("AI");

            fighter1Portrait.sprite = Resources.Load<Sprite>($"Portraits/{fighter1.GetComponent<FighterStats>().Name}");
            fighter2Portrait.sprite = Resources.Load<Sprite>($"Portraits/{fighter2.GetComponent<FighterStats>().Name}");
        }
        else if (menu.gameMode == "duel")
        {
            fighter1 = GameObject.FindGameObjectWithTag("Player1");
            fighter2 = GameObject.FindGameObjectWithTag("Player2");

            fighter1Portrait.sprite = Resources.Load<Sprite>($"Portraits/{fighter1.GetComponent<FighterStats>().Name}");
            fighter2Portrait.sprite = Resources.Load<Sprite>($"Portraits/{fighter2.GetComponent<FighterStats>().Name}");
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
