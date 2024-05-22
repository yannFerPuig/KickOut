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
                    stats.attackPoint.transform.position = new Vector3(player.transform.position.x + 0.25f, player.transform.position.y + 1.1f, stats.attackPointPos.z); 
                    stats.groundCheck.transform.position = new Vector3(player.transform.position.x, player.transform.position.y - 1.8f, player.transform.position.z);

                    CapsuleCollider2D capsuleCollider2D = player.GetComponent<CapsuleCollider2D>();
                    capsuleCollider2D.size = new Vector2(stats.width, stats.height);

                    PlayerAttack playerAttack = player.GetComponent<PlayerAttack>();
                    playerAttack.punch = Resources.Load<AnimationClip>("Animation/Carmen/carmenAttack");
                    playerAttack.special = Resources.Load<AnimationClip>("Animation/Carmen/carmenSpecial");

                    break;

                case "Louis":
                    player.GetComponent<SpriteRenderer>().sprite = Resources.Load<Sprite>("BaseSprites/Louis");

                    player.gameObject.AddComponent(typeof(LouisStats));
                    Animator animatorL = player.gameObject.AddComponent(typeof(Animator)) as Animator;
                    RuntimeAnimatorController runtimeAnimatorControllerL = Resources.Load<RuntimeAnimatorController>("Animation/Louis/Louis");
                    animatorL.runtimeAnimatorController = runtimeAnimatorControllerL;


                    LouisStats statsL = player.GetComponent<LouisStats>();
                    statsL.Initialize();

                    player.transform.position = new Vector3(statsL.spawnPoint.x, statsL.spawnPoint.y, statsL.spawnPoint.z);
                    statsL.attackPoint.transform.position = new Vector3(player.transform.position.x + 0.25f, player.transform.position.y + 1.1f, statsL.attackPointPos.z); 
                    statsL.groundCheck.transform.position = new Vector3(player.transform.position.x, player.transform.position.y - 1.8f, player.transform.position.z);

                    CapsuleCollider2D capsuleCollider2DL = player.GetComponent<CapsuleCollider2D>();
                    capsuleCollider2DL.size = new Vector2(statsL.width, statsL.height);

                    PlayerAttack playerAttackL = player.GetComponent<PlayerAttack>();
                    playerAttackL.punch = Resources.Load<AnimationClip>("Animation/Louis/louisAttack");
                    playerAttackL.special = Resources.Load<AnimationClip>("Animation/Louis/louisSpecial");

                    break;
                
                case "Bob Un":
                    player.GetComponent<SpriteRenderer>().sprite = Resources.Load<Sprite>("BaseSprites/Bob Un");

                    player.gameObject.AddComponent(typeof(BobUnStats));
                    Animator animatorB = player.gameObject.AddComponent(typeof(Animator)) as Animator;
                    RuntimeAnimatorController runtimeAnimatorControllerB = Resources.Load<RuntimeAnimatorController>("Animation/BobUn/Bob Un");
                    animatorB.runtimeAnimatorController = runtimeAnimatorControllerB;


                    BobUnStats statsB = player.GetComponent<BobUnStats>();
                    statsB.Initialize();

                    player.transform.position = new Vector3(statsB.spawnPoint.x, statsB.spawnPoint.y, statsB.spawnPoint.z);
                    statsB.attackPoint.transform.position = new Vector3(player.transform.position.x + 0.25f, player.transform.position.y + 1.1f, statsB.attackPointPos.z); 
                    statsB.groundCheck.transform.position = new Vector3(player.transform.position.x, player.transform.position.y - 1.8f, player.transform.position.z);

                    CapsuleCollider2D capsuleCollider2DB = player.GetComponent<CapsuleCollider2D>();
                    capsuleCollider2DB.size = new Vector2(statsB.width, statsB.height);

                    PlayerAttack playerAttackB = player.GetComponent<PlayerAttack>();
                    playerAttackB.punch = Resources.Load<AnimationClip>("Animation/BobUn/bobUnAttack");
                    playerAttackB.special = Resources.Load<AnimationClip>("Animation/BobUn/bobUnSpecial");

                    break;
            }
        }
    }
}