using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MainMenu : MonoBehaviour
{
    public GameObject img;
    public List<Sprite> sprites;

    public string gameMode;


    GameObject[] menuButtons;
    GameObject[] modeButtons;

    public void Start()
    {
        menuButtons = GameObject.FindGameObjectsWithTag("MenuButton");
        modeButtons = GameObject.FindGameObjectsWithTag("ModeButton");
        Debug.Log(modeButtons.Length);

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
        gameMode = "solo";
        SceneManager.LoadScene("SoloCharacterSelection");
    }

    public void Multiplayer()
    {
        gameMode = "duel";
        SceneManager.LoadScene("MultiplayerCharacterSelection");
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

    public void NextSprite(int index)
    {
        img.GetComponent<Image>().sprite = sprites[index];
    }

    public void StartFight() 
    {
        SceneManager.LoadScene("FightScene");
    }
}