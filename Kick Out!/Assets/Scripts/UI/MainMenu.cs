using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using TMPro;

public class MainMenu : MonoBehaviour
{
    //SCRIPTS
    RoundTimer roundTimer;
    RoundManager roundManager;

    //GameObjects
    GameObject[] menuButtons;
    GameObject[] modeButtons;
    GameObject roundWinner;

    public GameObject player1;

    public TextMeshProUGUI fightWinner;

    //DATA
    public string fighterSelected;
    public string fighter1;
    public string fighter2;
    public string gameMode;


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
        menuButtons = GameObject.FindGameObjectsWithTag("MenuButton");
        modeButtons = GameObject.FindGameObjectsWithTag("ModeButton");

        foreach (GameObject modeButton in modeButtons)
        {
            modeButton.SetActive(false);
        }

        roundTimer = gameObject.GetComponent<RoundTimer>();
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
        SceneManager.LoadScene("MultiCharacterSelection");
    }

    public void Tutorial()
    {
        gameMode = "tutorial";
        SceneManager.LoadScene("SoloCharacterSelection");
    }

    public void EndFight()
    {
        SceneManager.LoadScene("Winner");
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

    void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        if (scene.name == "FightScene" || scene.name == "Tutorial" || scene.name == "FightSceneMultiplayer") 
        {
            gameObject.AddComponent<RoundManager>();
            gameObject.AddComponent<RoundTimer>();
            gameObject.AddComponent<Fight>();

            if (gameMode == "tutorial")
            {
                GameObject player = GameObject.FindGameObjectWithTag("Player");

                LoadFighter(player, fighterSelected);
            }
            else if (gameMode == "solo")
            {
                GameObject player = GameObject.FindGameObjectWithTag("Player");
                GameObject ai = GameObject.FindGameObjectWithTag("AI");
                
                LoadFighter(ai, ChooseAIFighter());
                LoadFighter(player, fighterSelected);
            }
        }

        if (scene.name == "FightSceneMultiplayer")
        {
            if (gameMode == "duel")
            {
                player1 = GameObject.FindGameObjectWithTag("Player1");
                GameObject player2 = GameObject.FindGameObjectWithTag("Player2");

                LoadFighter(player1, fighter1);
                LoadFighter(player2, fighter2);
            }
        }

        if (scene.name == "Winner")
        {
            fightWinner = GameObject.Find("WinnerName").GetComponent<TextMeshProUGUI>();

            fightWinner.text = gameObject.GetComponent<RoundManager>().fightWinner + "(" + gameObject.GetComponent<RoundManager>().tagWinner + ")";

            Destroy(gameObject.GetComponent<RoundManager>());
            Destroy(gameObject.GetComponent<RoundTimer>());
            Destroy(gameObject.GetComponent<Fight>());
        }
    }

    public void LoadFighter(GameObject p, string fighter)
    {
        SpriteRenderer spriteRenderer = p.GetComponent<SpriteRenderer>();
        Animator animator = p.gameObject.AddComponent(typeof(Animator)) as Animator;
        
        Attack playerAttack = p.GetComponent<Attack>();

        switch (fighter)
        {
            case "Carmen":
                spriteRenderer.sprite = Resources.Load<Sprite>("BaseSprites/Carmen");

                CarmenStats carmenStats = p.gameObject.AddComponent(typeof(CarmenStats)) as CarmenStats;
                carmenStats.Initialize();

                RuntimeAnimatorController runtimeAnimatorController = Resources.Load<RuntimeAnimatorController>("Animation/Carmen/Carmen");
                animator.runtimeAnimatorController = runtimeAnimatorController;

                playerAttack.punch = Resources.Load<AnimationClip>("Animation/Carmen/carmenAttack");
                playerAttack.special = Resources.Load<AnimationClip>("Animation/Carmen/carmenSpecial");

                SetPlayerPosition(p, carmenStats);

                carmenStats.attackPoint.transform.position = new Vector3(p.transform.position.x + carmenStats.attackPointPos.x, p.transform.position.y + carmenStats.attackPointPos.y * 5, carmenStats.attackPointPos.z); 
                carmenStats.groundCheck.transform.position = new Vector3(p.transform.position.x + carmenStats.attackPointPos.x, p.transform.position.y + carmenStats.groundCheckPointPos.y * 5, p.transform.position.z);
                carmenStats.center.transform.position = new Vector3(p.transform.position.x + carmenStats.fighterCenter.x, p.transform.position.y + carmenStats.fighterCenter.y, p.transform.position.z + carmenStats.fighterCenter.z);

                CapsuleCollider2D capsuleCollider2DC = p.GetComponent<CapsuleCollider2D>();
                capsuleCollider2DC.size = new Vector2(carmenStats.width, carmenStats.height);

                break;

            case "Louis":
                spriteRenderer.sprite = Resources.Load<Sprite>("BaseSprites/Louis");

                LouisStats louisStats = p.gameObject.AddComponent(typeof(LouisStats)) as LouisStats;
                louisStats.Initialize();

                RuntimeAnimatorController runtimeAnimatorControllerL = Resources.Load<RuntimeAnimatorController>("Animation/Louis/Louis");
                animator.runtimeAnimatorController = runtimeAnimatorControllerL;

                playerAttack.punch = Resources.Load<AnimationClip>("Animation/Louis/louisAttack");
                playerAttack.special = Resources.Load<AnimationClip>("Animation/Louis/louisSpecial");
                
                SetPlayerPosition(p, louisStats);

                louisStats.attackPoint.transform.position = new Vector3(p.transform.position.x, p.transform.position.y + louisStats.attackPointPos.y * 5, louisStats.attackPointPos.z); 
                louisStats.groundCheck.transform.position = new Vector3(p.transform.position.x, p.transform.position.y + louisStats.groundCheckPointPos.y * 5, p.transform.position.z);
                louisStats.center.transform.position = new Vector3(p.transform.position.x + louisStats.fighterCenter.x * 5, p.transform.position.y + louisStats.fighterCenter.y * 5, p.transform.position.z + louisStats.fighterCenter.z);

                CapsuleCollider2D capsuleCollider2DL = p.GetComponent<CapsuleCollider2D>();
                capsuleCollider2DL.size = new Vector2(louisStats.width, louisStats.height);

                break;
            
            case "Bob Un":
                p.GetComponent<SpriteRenderer>().sprite = Resources.Load<Sprite>("BaseSprites/Bob Un");

                BobUnStats bobUnStats = p.gameObject.AddComponent(typeof(BobUnStats)) as BobUnStats;
                bobUnStats.Initialize();

                RuntimeAnimatorController runtimeAnimatorControllerB = Resources.Load<RuntimeAnimatorController>("Animation/BobUn/Bob Un");
                animator.runtimeAnimatorController = runtimeAnimatorControllerB;

                playerAttack.punch = Resources.Load<AnimationClip>("Animation/BobUn/bobUnAttack");
                playerAttack.special = Resources.Load<AnimationClip>("Animation/BobUn/bobUnSpecial");

                SetPlayerPosition(p, bobUnStats);

                bobUnStats.attackPoint.transform.position = new Vector3(p.transform.position.x, p.transform.position.y + bobUnStats.attackPointPos.y * 5, bobUnStats.attackPointPos.z); 
                bobUnStats.groundCheck.transform.position = new Vector3(p.transform.position.x, p.transform.position.y + bobUnStats.groundCheckPointPos.y * 5, p.transform.position.z);

                CapsuleCollider2D capsuleCollider2DB = p.GetComponent<CapsuleCollider2D>();
                capsuleCollider2DB.size = new Vector2(bobUnStats.width, bobUnStats.height);

                break;
        }
    }

    private void SetPlayerPosition(GameObject player, FighterStats stats)
    {
        if (gameMode == "tutorial")
        {
            player.transform.position = new Vector3(stats.spawnPoint.x, stats.spawnPoint.y, stats.spawnPoint.z);
        }
        else if (gameMode == "solo")
        {
            if (player.tag == "Player")
            {
                player.transform.position = new Vector3(stats.spawnPoint.x, stats.spawnPoint.y, stats.spawnPoint.z);
            }
            else if (player.tag == "AI")
            {
                player.transform.position = new Vector3(-stats.spawnPoint.x, stats.spawnPoint.y, stats.spawnPoint.z);
            }
        }
        else if (gameMode == "duel")
        {
            if (player.tag == "Player1")
            {
                player.transform.position = new Vector3(stats.spawnPoint.x, stats.spawnPoint.y, stats.spawnPoint.z);
            }
            else if (player.tag == "Player2")
            {
                player.transform.position = new Vector3(-stats.spawnPoint.x, stats.spawnPoint.y, stats.spawnPoint.z);
            }
        }
    }

    public string ChooseAIFighter()
    {
        int fighter = Random.Range(0, 3);

        if (fighter == 0)
        {
            return "Carmen"; 
        }
        else if (fighter == 1)
        {
            return "Louis";
        }
        else 
        {
            return "Bob Un";
        }
    }

    public void WinRound(Fighter fighter)
    {
        roundWinner.SetActive(true);
        roundWinner.GetComponent<Text>().text = fighter.name;

        if (gameMode == "solo")
        {
            if (fighter.CompareTag("Player"))
            {
                fighter.transform.position = new Vector3(fighter.stats.spawnPoint.x, fighter.stats.spawnPoint.y, fighter.stats.spawnPoint.z);
            }
            else
            {
                fighter.transform.position = new Vector3(-fighter.stats.spawnPoint.x, fighter.stats.spawnPoint.y, fighter.stats.spawnPoint.z);    
            }
        }
        else
        {
            if (fighter.CompareTag("Player1"))
            {
                fighter.transform.position = new Vector3(fighter.stats.spawnPoint.x, fighter.stats.spawnPoint.y, fighter.stats.spawnPoint.z);
            }
            else
            {
                fighter.transform.position = new Vector3(-fighter.stats.spawnPoint.x, fighter.stats.spawnPoint.y, fighter.stats.spawnPoint.z);    
            }
        }

        roundTimer.Start();
        roundTimer.Update();
    }

    public void OnDrawGizmosSelected()
    {
        Gizmos.DrawCube(new Vector3(0, 0, 0), new Vector3(5, 5, 0));
    }
}