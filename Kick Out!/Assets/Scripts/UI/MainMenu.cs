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

    //ANIMATORS CONTROLLERS
    public RuntimeAnimatorController carmenController;

    void Awake()
    {
        DontDestroyOnLoad(gameObject);
    }

    void OnEnable()
    {
        SceneManager.sceneLoaded += OnSceneLoaded;
    }

    void OnDisable()
    {
        SceneManager.sceneLoaded -= OnSceneLoaded;
    }

    public void Start()
    {
        img = GameObject.FindGameObjectWithTag("FighterSelected");

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
                    player.GetComponent<SpriteRenderer>().sprite = Resources.Load<Sprite>("BaseSprites/Carmen");

                    player.gameObject.AddComponent(typeof(CarmenStats));
                    Animator animator = player.gameObject.AddComponent(typeof(Animator)) as Animator;
                    RuntimeAnimatorController runtimeAnimatorController = Resources.Load<RuntimeAnimatorController>("Animation/Carmen/Carmen");
                    animator.runtimeAnimatorController = runtimeAnimatorController;


                    CarmenStats stats = player.GetComponent<CarmenStats>();
                    stats.Initialize();

                    player.transform.position = new Vector3(stats.spawnPoint.x, stats.spawnPoint.y, stats.spawnPoint.z);

                    stats.groundCheck.position = new Vector3(0, -0.3f, 0);

                    Transform attackPoint = player.transform.Find("AttackPoint");
                    attackPoint.position = stats.attackPointPos;

                    CapsuleCollider2D capsuleCollider2D = player.GetComponent<CapsuleCollider2D>();
                    capsuleCollider2D.size = new Vector2(stats.width, stats.height);

                    PlayerAttack playerAttack = player.GetComponent<PlayerAttack>();
                    playerAttack.punch = Resources.Load<AnimationClip>("Animation/Carmen/carmenPunch");
                    playerAttack.special = Resources.Load<AnimationClip>("Animation/Carmen/carmenSpecial");

                    break;
            }
        }
    }
}