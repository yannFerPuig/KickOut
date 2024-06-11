using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using TMPro;
using Unity.VisualScripting.Antlr3.Runtime.Misc;
using UnityEngine.UIElements;

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
        SpriteRenderer spriteRenderer = player.GetComponent<SpriteRenderer>();
        Animator animator = player.gameObject.AddComponent(typeof(Animator)) as Animator;
        
        Attack playerAttack = player.GetComponent<Attack>();

        switch (fighter)
        {
            case "Carmen":
                spriteRenderer.sprite = Resources.Load<Sprite>("BaseSprites/Carmen");

                player.gameObject.AddComponent(typeof(CarmenStats));
                RuntimeAnimatorController runtimeAnimatorController = Resources.Load<RuntimeAnimatorController>("Animation/Carmen/Carmen");
                animator.runtimeAnimatorController = runtimeAnimatorController;

                playerAttack.punch = Resources.Load<AnimationClip>("Animation/Carmen/carmenAttack");
                playerAttack.special = Resources.Load<AnimationClip>("Animation/Carmen/carmenSpecial");

                break;

            case "Louis":
                spriteRenderer.sprite = Resources.Load<Sprite>("BaseSprites/Louis");

                player.gameObject.AddComponent(typeof(LouisStats));

                RuntimeAnimatorController runtimeAnimatorControllerL = Resources.Load<RuntimeAnimatorController>("Animation/Louis/Louis");
                animator.runtimeAnimatorController = runtimeAnimatorControllerL;

                playerAttack.punch = Resources.Load<AnimationClip>("Animation/Louis/louisAttack");
                playerAttack.special = Resources.Load<AnimationClip>("Animation/Louis/louisSpecial");

                break;
            
            case "Bob Un":
                player.GetComponent<SpriteRenderer>().sprite = Resources.Load<Sprite>("BaseSprites/Bob Un");

                player.gameObject.AddComponent(typeof(BobUnStats));

                RuntimeAnimatorController runtimeAnimatorControllerB = Resources.Load<RuntimeAnimatorController>("Animation/BobUn/Bob Un");
                animator.runtimeAnimatorController = runtimeAnimatorControllerB;

                playerAttack.punch = Resources.Load<AnimationClip>("Animation/BobUn/bobUnAttack");
                playerAttack.special = Resources.Load<AnimationClip>("Animation/BobUn/bobUnSpecial");

                break;
        }

        FighterStats fighterStats = player.GetComponent<FighterStats>();
        
        if (fighterStats is CarmenStats stats)
        {
            stats.Initialize();
        }
        else if (fighterStats is LouisStats stats1)
        {
            stats1.Initialize();
        }
        else if (fighterStats is BobUnStats stats2)
        {
            stats2.Initialize();
        }

        if (gameMode == "tutorial")
        {
            player.transform.position = new Vector3(fighterStats.spawnPoint.x, fighterStats.spawnPoint.y, fighterStats.spawnPoint.z);
        }
        else if (gameMode == "solo" || gameMode == "duel")
        {
            if (player.tag == "Player" || player.tag == "Player1")
            {
                player.transform.position = new Vector3(fighterStats.spawnPoint.x, fighterStats.spawnPoint.y, fighterStats.spawnPoint.z);
            }
            else if (player.tag == "AI" || player.tag == "Player2")
            {
                player.transform.position = new Vector3(-fighterStats.spawnPoint.x, fighterStats.spawnPoint.y, fighterStats.spawnPoint.z);
            }
        }

        fighterStats.attackPoint.transform.position = new Vector3(player.transform.position.x, player.transform.position.y + fighterStats.attackPointPos.y * 5, fighterStats.attackPointPos.z); 
        fighterStats.groundCheck.transform.position = new Vector3(player.transform.position.x, player.transform.position.y + fighterStats.groundCheckPointPos.y * 5, player.transform.position.z);

        CapsuleCollider2D capsuleCollider2D = player.GetComponent<CapsuleCollider2D>();
        capsuleCollider2D.size = new Vector2(fighterStats.width, fighterStats.height);
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