using System.Collections;
using System.Collections.Generic;
using Mono.Cecil;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MainMenu : MonoBehaviour
{

    //GameObjects
    GameObject img;
    GameObject[] menuButtons;
    GameObject[] modeButtons;


    public string fighterSelected;
    public string gameMode;

    //SPRITES
    public Sprite CarmenSprite;

    void Awake()
    {
        DontDestroyOnLoad(gameObject);
    }

    public void Start()
    {
        img = GameObject.FindGameObjectWithTag("FighterSelected");

        CarmenSprite = Resources.Load<Sprite>("carmenBase.psd");

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
        SceneManager.LoadScene("NETWORK");
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

    public void SelectFighterSprite(Sprite sprite)
    {
        img.GetComponent<Image>().sprite = sprite;
    }

    public void SelectFighterName(string name)
    {
        fighterSelected = name;
    }

    public void StartFight() 
    {
        SceneManager.LoadScene("FightScene");
    }

    void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        if (scene.name == "FightScene")
        {
            GameObject player = GameObject.FindGameObjectWithTag("Player");

            switch (fighterSelected)
            {
                case "Carmen":
                    player.gameObject.AddComponent(typeof(CarmenStats));
                    player.GetComponent<SpriteRenderer>().sprite = Resources.Load<Sprite>("carmenBase");
                    break;
            }
        }
    }
}