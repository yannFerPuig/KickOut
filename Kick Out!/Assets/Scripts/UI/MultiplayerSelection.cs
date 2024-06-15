using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using TMPro;

public class MultiplayerSelection : MonoBehaviour
{
    //SCRIPTS
    public MainMenu menu;

    //GAMEOBJECTS
    GameObject fighterSelected1;
    GameObject fighterSelected2;
    GameObject startButton;
    GameObject lockButton;

    //UI
    TextMeshProUGUI textPlayer1;
    TextMeshProUGUI textPlayer2;

    //DATA
    string currentPlayer = "Player 1";
    string player1Selection;
    string player2Selection;

    bool canSelect = true;
    bool hasPlayer1Selected;
    bool hasPlayer2Selected;

    void Start()
    {
        menu = GameObject.FindGameObjectWithTag("MainCamera").GetComponent<MainMenu>();

        fighterSelected1 = GameObject.FindGameObjectWithTag("FighterSelected1");
        fighterSelected2 = GameObject.FindGameObjectWithTag("FighterSelected2");

        startButton = GameObject.FindGameObjectWithTag("StartButton");
        lockButton = GameObject.FindGameObjectWithTag("LockButton");

        textPlayer1 = GameObject.FindGameObjectWithTag("TextPlayer1").GetComponent<TextMeshProUGUI>();
        textPlayer2 = GameObject.FindGameObjectWithTag("TextPlayer2").GetComponent<TextMeshProUGUI>();

        textPlayer1.color = Color.white;
        textPlayer2.color = Color.white;

        startButton.SetActive(false);
    }

    void Update()
    {
        if (hasPlayer1Selected && hasPlayer2Selected)
        {
            lockButton.SetActive(false);
            startButton.SetActive(true);
        }

        if (currentPlayer == "Player 1")
        {
            textPlayer1.color = new Color(1f, 0.5f, 0.15f);
        }
        else if (currentPlayer == "Player 2")
        {
            textPlayer2.color = new Color(1f, 0.5f, 0.15f);
        }
    }

    public void SelectFighterSprite(Sprite sprite)
    {
        if (currentPlayer == "Player 1" && canSelect)
        {    
            fighterSelected1.GetComponent<Image>().sprite = sprite;
        }
        else if (currentPlayer == "Player 2" && canSelect) 
        {
            fighterSelected2.GetComponent<Image>().sprite = sprite;
        }
    }

    public void SelectFighterName(string name)
    {
        if (currentPlayer == "Player 1" && canSelect)
        {    
            menu.fighter1 = name;
        }
        else if (currentPlayer == "Player 2" && canSelect) 
        {
            menu.fighter2 = name;
        }
    }

    public void Lock()
    {
        if (currentPlayer == "Player 1")
        {
            currentPlayer = "Player 2";
            hasPlayer1Selected = true;
        }
        else if (currentPlayer == "Player 2")
        {
            currentPlayer = "";
            hasPlayer2Selected = true;
            canSelect = false;
        }
    }

    public void StartFight() 
    {
        if (menu.fighterSelected != "")
        {    
            SceneManager.LoadScene("FightSceneMultiplayer");
        }
    }
}
