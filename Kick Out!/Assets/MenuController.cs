using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuController : MonoBehaviour
{
    
    public MainMenu menu;

    public GameObject[] menuButtons;
    public GameObject[] modeButtons;


    // Start is called before the first frame update
    void Awake()
    {
        menu = GameObject.FindGameObjectWithTag("MainCamera").GetComponent<MainMenu>();

        menuButtons = GameObject.FindGameObjectsWithTag("MenuButton");
        modeButtons = GameObject.FindGameObjectsWithTag("ModeButton");

        foreach (GameObject modeButton in modeButtons)
        {
            modeButton.SetActive(false);
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void SelectMode() 
    {
        foreach (GameObject modeButton in modeButtons)
        {
            modeButton.SetActive(true);
        }

        foreach (GameObject menuButton in menuButtons)
        {
            menuButton.SetActive(false);
        }
    }

    public void CancelModeSelection() 
    {
        foreach (GameObject menuButton in menuButtons)
        {
            menuButton.SetActive(true);
        }

        foreach (GameObject modeButton in modeButtons)
        {
            modeButton.SetActive(false);
        }
    }

    public void ChangeScene(string scene)
    {
        SceneManager.LoadSceneAsync(scene);
    }   

    public void Exit()
    {
        Application.Quit();
    }

    public void Computer() 
    {
        menu.gameMode = "solo";
        SceneManager.LoadScene("SoloCharacterSelection");
    }

    public void Multiplayer()
    {
        menu.gameMode = "duel";
        SceneManager.LoadScene("MultiCharacterSelection");
    }

    public void Tutorial()
    {
        menu.gameMode = "tutorial";
        SceneManager.LoadScene("SoloCharacterSelection");
    }
}
