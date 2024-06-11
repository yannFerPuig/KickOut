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
    GameObject selected;
    GameObject[] menuButtons;
    GameObject[] modeButtons;
    GameObject roundWinner;

    public TextMeshProUGUI fightWinner;

    //DATA
    public string fighterSelected;
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
        SceneManager.LoadScene("NETWORK");
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
        if (scene.name == "SoloCharacter")
        {
            selected = GameObject.FindGameObjectWithTag("FighterSelected");
        }

        if (scene.name == "FightScene" || scene.name == "Tutorial") 
        {
            gameObject.AddComponent<RoundManager>();
            gameObject.AddComponent<RoundTimer>();

            roundWinner = GameObject.Find("RoundWinner");

            if (scene.name == "FightScene")
            {
                roundWinner.SetActive(false);
            }

            if (gameMode == "tutorial")
            {
                GameObject player = GameObject.FindGameObjectWithTag("Player");

                LoadFighter(player, fighterSelected);
            }
            else if (gameMode == "solo")
            {
                GameObject player = GameObject.FindGameObjectWithTag("Player");
                GameObject ai = GameObject.FindGameObjectWithTag("AI");
                
                LoadFighter(player, fighterSelected);
                LoadFighter(ai, ChooseAIFighter());
            }
            else if (gameMode == "duel")
            {
                GameObject player1 = GameObject.FindGameObjectWithTag("Player1");
                GameObject player2 = GameObject.FindGameObjectWithTag("Player2");

                LoadFighter(player1, fighterSelected);
                LoadFighter(player2, fighterSelected);
            }
        }

        if (scene.name == "Winner")
        {
            fightWinner = GameObject.Find("WinnerName").GetComponent<TextMeshProUGUI>();

            fightWinner.text = gameObject.GetComponent<RoundManager>().fightWinner;

            Destroy(gameObject.GetComponent<RoundManager>());
            Destroy(gameObject.GetComponent<RoundTimer>());
        }
    }

    public void LoadFighter(GameObject player, string fighter)
    {
        switch (fighter)
        {
            case "Carmen":
                player.GetComponent<SpriteRenderer>().sprite = Resources.Load<Sprite>("BaseSprites/Carmen");

                player.gameObject.AddComponent(typeof(CarmenStats));
                Animator animator = player.gameObject.AddComponent(typeof(Animator)) as Animator;
                RuntimeAnimatorController runtimeAnimatorController = Resources.Load<RuntimeAnimatorController>("Animation/Carmen/Carmen");
                animator.runtimeAnimatorController = runtimeAnimatorController;

                CarmenStats stats = player.GetComponent<CarmenStats>();
                stats.Initialize();

                if (gameMode == "solo" || gameMode == "tutorial")
                {
                    player.transform.position = new Vector3(stats.spawnPoint.x, stats.spawnPoint.y, stats.spawnPoint.z);
                }
                else
                {
                    if (player.CompareTag("Player1"))
                    {
                        player.transform.position = new Vector3(stats.spawnPoint.x, stats.spawnPoint.y, stats.spawnPoint.z);
                    }
                    else 
                    {
                        player.transform.position = new Vector3(-stats.spawnPoint.x, stats.spawnPoint.y, stats.spawnPoint.z);
                    }
                }

                stats.attackPoint.transform.position = new Vector3(player.transform.position.x, player.transform.position.y + 1.1f, stats.attackPointPos.z); 
                stats.groundCheck.transform.position = new Vector3(player.transform.position.x, player.transform.position.y + stats.groundCheckPointPos.y * 5, player.transform.position.z);

                CapsuleCollider2D capsuleCollider2D = player.GetComponent<CapsuleCollider2D>();
                capsuleCollider2D.size = new Vector2(stats.width, stats.height);

                PlayerAttack playerAttack = player.GetComponent<PlayerAttack>();
                playerAttack.punch = Resources.Load<AnimationClip>("Animation/Carmen/carmenAttack");
                playerAttack.special = Resources.Load<AnimationClip>("Animation/Carmen/carmenSpecial");

                break;

            case "Louis":
                player.GetComponent<SpriteRenderer>().sprite = Resources.Load<Sprite>("BaseSprites/Louis");

                player.gameObject.AddComponent(typeof(LouisStats));

                LouisStats statsL = player.GetComponent<LouisStats>();
                statsL.Initialize();

                Animator animatorL = player.gameObject.AddComponent(typeof(Animator)) as Animator;
                RuntimeAnimatorController runtimeAnimatorControllerL = Resources.Load<RuntimeAnimatorController>("Animation/Louis/Louis");
                animatorL.runtimeAnimatorController = runtimeAnimatorControllerL;

                if (gameMode == "solo" || gameMode == "tutorial")
                {
                    player.transform.position = new Vector3(statsL.spawnPoint.x, statsL.spawnPoint.y, statsL.spawnPoint.z);
                }
                else
                {
                    if (player.CompareTag("Player1"))
                    {
                        player.transform.position = new Vector3(statsL.spawnPoint.x, statsL.spawnPoint.y, statsL.spawnPoint.z);
                    }
                    else 
                    {
                        player.transform.position = new Vector3(-statsL.spawnPoint.x, statsL.spawnPoint.y, statsL.spawnPoint.z);
                    }
                }

                statsL.attackPoint.transform.position = new Vector3(player.transform.position.x, player.transform.position.y + statsL.attackPointPos.y * 5, statsL.attackPointPos.z); 
                statsL.groundCheck.transform.position = new Vector3(player.transform.position.x, player.transform.position.y + statsL.groundCheckPointPos.y * 5, player.transform.position.z);

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

                if (gameMode == "solo")
                {
                    player.transform.position = new Vector3(statsB.spawnPoint.x, statsB.spawnPoint.y, statsB.spawnPoint.z);
                }
                else
                {
                    if (player.CompareTag("Player1"))
                    {
                        player.transform.position = new Vector3(statsB.spawnPoint.x, statsB.spawnPoint.y, statsB.spawnPoint.z);
                    }
                    else 
                    {
                        player.transform.position = new Vector3(-statsB.spawnPoint.x, statsB.spawnPoint.y, statsB.spawnPoint.z);
                    }
                }

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

    public string ChooseAIFighter()
    {
        int fighter = Random.Range(0, 2);

        if (fighter == 0)
        {
            return "Carmen"; 
        }
        else if (fighter == 1)
        {
            return "Louis";
        }

        return "Bob Un";
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