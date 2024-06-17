using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuController : MonoBehaviour
{
    
    public MainMenu menu;

    public GameObject[] menuButtons;
    public GameObject[] modeButtons;

    public SoundDesign soundManager;

    // Start is called before the first frame update
    void Awake()
    {
        menu = GameObject.FindGameObjectWithTag("MainCamera").GetComponent<MainMenu>();
        soundManager = GameObject.FindGameObjectWithTag("SoundManager").GetComponent<SoundDesign>();

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
        soundManager.music.clip = Resources.Load<AudioClip>("Sound/KO-drumtrescool-selectionperso");
        soundManager.music.Play(); soundManager.music.loop = true;
    }

    public void Multiplayer()
    {
        menu.gameMode = "duel";
        SceneManager.LoadScene("MultiCharacterSelection");
        soundManager.music.clip = Resources.Load<AudioClip>("Sound/KO-drumtrescool-selectionperso");
        soundManager.music.Play(); soundManager.music.loop = true;
    }

    public void Network()
    {
        menu.gameMode = "network";
        SceneManager.LoadScene("ANIMATIONS");
        soundManager.music.clip = Resources.Load<AudioClip>("Sound/KO-drumtrescool-selectionperso");
        soundManager.music.Play(); soundManager.music.loop = true;
    }

    public void Tutorial()
    {
        menu.gameMode = "tutorial";
        SceneManager.LoadScene("SoloCharacterSelection");
        soundManager.music.clip = Resources.Load<AudioClip>("Sound/KO-drumtrescool-selectionperso");
        soundManager.music.Play(); soundManager.music.loop = true;
    }
}
