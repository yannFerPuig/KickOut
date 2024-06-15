using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class FighterSelection : MonoBehaviour
{
    //SCRIPTS
    public MainMenu mainMenu;

    //GAMEOBJECTS
    public GameObject fighterSelected;

    void Start()
    {
        mainMenu = GameObject.FindGameObjectWithTag("MainCamera").GetComponent<MainMenu>();
        fighterSelected = GameObject.FindGameObjectWithTag("FighterSelected");
    }

    public void SelectFighterSprite(Sprite sprite)
    {
        fighterSelected.GetComponent<Image>().sprite = sprite;
    }

    public void SelectFighterName(string name)
    {
        mainMenu.fighterSelected = name;
    }

    public void StartFight() 
    {
        if (mainMenu.fighterSelected != "")
        {
            if (mainMenu.gameMode == "solo") 
            {
                SceneManager.LoadScene("FightScene");
            } 
            else if (mainMenu.gameMode == "tutorial") 
            {
                SceneManager.LoadScene("Tutorial");
            }
        }
    }
}
