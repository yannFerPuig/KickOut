using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using TMPro;
using Unity.VisualScripting;
using System;

public class MainMenu : MonoBehaviour
{
    // SCRIPTS
    RoundTimer roundTimer;
    RoundManager roundManager;

    // GameObjects
    GameObject[] menuButtons;
    GameObject[] modeButtons;
    GameObject roundWinner;

    public SoundDesign soundManager;

    public GameObject player1;
    public TextMeshProUGUI fightWinner;

    // DATA
    public string fighterSelected;
    public string fighter1;
    public string fighter2;
    public string gameMode;

    // Singleton instance
    private static MainMenu instance;

    void Awake()
    {
        // Ensure only one instance of MainMenu exists
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else if (instance != this)
        {
            Destroy(gameObject);
            return;
        }

        // Subscribe to the sceneLoaded event
        SceneManager.sceneLoaded += OnSceneLoaded;
    }

    void OnEnable()
    {
        SceneManager.sceneLoaded += OnSceneLoaded;
    }

    void OnDisable()
    {
        SceneManager.sceneLoaded -= OnSceneLoaded;
    }

    void Start()
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

        if (scene == "Menu")
        {
            soundManager.music.clip = Resources.Load<AudioClip>("Sound/KO-drill fivio-menu");
        }
        else if (scene == "MultiCharacterSelection" || scene == "SoloCharacterSelection")
        {
            soundManager.music.clip = Resources.Load<AudioClip>("Sound/KO-drumtrescool-selectionperso");
        }
        else if(scene == "FightScene" || scene == "FightSceneMultiplayer")
        {
            soundManager.music.clip = Resources.Load<AudioClip>("Sound/KO-NYK BIEN-combat");
        }
        else if(scene == "Winner")
        {
            soundManager.music.clip = Resources.Load<AudioClip>("Sound/KO-SOUTHSIDE-fin de jeu");
        }
    }

    public void ChangeMusic(AudioClip clip)
    {
        soundManager.music.clip = clip;
        soundManager.music.Play(); soundManager.music.loop = true;
    }

 

    public void Exit()
    {
        Application.Quit();
    }

    public void Computer()
    {
        gameMode = "solo";
        SceneManager.LoadScene("SoloCharacterSelection");
        soundManager.music.clip = Resources.Load<AudioClip>("Sound/KO-drumtrescool-selectionperso");
        soundManager.music.Play(); soundManager.music.loop = true;
    }

    public void Multiplayer()
    {
        gameMode = "duel";
        SceneManager.LoadScene("MultiCharacterSelection");
        soundManager.music.clip = Resources.Load<AudioClip>("Sound/KO-drumtrescool-selectionperso");
        soundManager.music.Play(); soundManager.music.loop = true;
    }

    public void Tutorial()
    {
        gameMode = "tutorial";
        SceneManager.LoadScene("SoloCharacterSelection");
        soundManager.music.clip = Resources.Load<AudioClip>("Sound/KO-drumtrescool-selectionperso");
        soundManager.music.Play(); soundManager.music.loop = true;
    }

    public void EndFight()
    {
        SceneManager.LoadScene("Winner");
        soundManager.music.clip = Resources.Load<AudioClip>("Sound/KO-SOUTHSIDE-fin de jeu");
        soundManager.music.Play(); soundManager.music.loop = true;
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
            Console.WriteLine("test1");
        }

        foreach (GameObject modeButton in modeButtons)
        {
            modeButton.SetActive(false);
            Console.WriteLine("test2");
        }
    }

    void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        if (scene.name == "FightScene" || scene.name == "Tutorial" || scene.name == "FightSceneMultiplayer")
        {
            AddComponentIfNotExists<RoundManager>(gameObject);
            AddComponentIfNotExists<RoundTimer>(gameObject);
            AddComponentIfNotExists<Fight>(gameObject);

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


    public void SetProfilePicture(string fighter,Image image)
    {
        switch(fighter)
        {
            case "Carmen":
                image.sprite = Resources.Load<Sprite>("Portraits/Carmen");
                break;
            case "Louis":
                image.sprite = Resources.Load<Sprite>("Portraits/Louis");
                break;
            case "Bob Un":
                image.sprite = Resources.Load<Sprite>("Portraits/Bob Un");
                break;
        }
    }


    public void LoadFighter(GameObject p, string fighter)
    {
        SpriteRenderer spriteRenderer = p.GetComponent<SpriteRenderer>();
        Attack playerAttack = p.GetComponent<Attack>();

        switch (fighter)
        {
            case "Carmen":
                spriteRenderer.sprite = Resources.Load<Sprite>("BaseSprites/Carmen");
               
                CarmenStats carmenStats = AddComponentIfNotExists<CarmenStats>(p);
                carmenStats.Initialize();

                SetAnimatorAndAttack(p, "Animation/Carmen/Carmen", "Animation/Carmen/carmenAttack", "Animation/Carmen/carmenSpecial");
                SetPlayerPosition(p, carmenStats);
                SetFighterPoints(p, carmenStats);
                break;

            case "Louis":
                spriteRenderer.sprite = Resources.Load<Sprite>("BaseSprites/Louis");
                LouisStats louisStats = AddComponentIfNotExists<LouisStats>(p);
                louisStats.Initialize();
                SetAnimatorAndAttack(p, "Animation/Louis/Louis", "Animation/Louis/louisAttack", "Animation/Louis/louisSpecial");
                SetPlayerPosition(p, louisStats);
                SetFighterPoints(p, louisStats);
                break;

            case "Bob Un":
                spriteRenderer.sprite = Resources.Load<Sprite>("BaseSprites/Bob Un");
                BobUnStats bobUnStats = AddComponentIfNotExists<BobUnStats>(p);
                bobUnStats.Initialize();
                SetAnimatorAndAttack(p, "Animation/BobUn/Bob Un", "Animation/BobUn/bobUnAttack", "Animation/BobUn/bobUnSpecial");
                SetPlayerPosition(p, bobUnStats);
                SetFighterPoints(p, bobUnStats);
                break;
        }
    }

    private void SetAnimatorAndAttack(GameObject player, string animatorPath, string attackPath, string specialPath)
    {
        Animator animator = player.GetComponent<Animator>();
        if (animator == null)
        {
            animator = player.AddComponent<Animator>();
        }
        animator.runtimeAnimatorController = Resources.Load<RuntimeAnimatorController>(animatorPath);

        Attack playerAttack = player.GetComponent<Attack>();
        if (playerAttack == null)
        {
            playerAttack = player.AddComponent<Attack>();
        }
        playerAttack.punch = Resources.Load<AnimationClip>(attackPath);
        playerAttack.special = Resources.Load<AnimationClip>(specialPath);
    }

    private void SetFighterPoints(GameObject player, FighterStats stats)
    {
        stats.attackPoint.transform.position = new Vector3(player.transform.position.x + stats.attackPointPos.x, player.transform.position.y + stats.attackPointPos.y * 5, stats.attackPointPos.z);
        stats.groundCheck.transform.position = new Vector3(player.transform.position.x + stats.groundCheckPointPos.x, player.transform.position.y + stats.groundCheckPointPos.y * 5, player.transform.position.z);
        stats.center.transform.position = new Vector3(player.transform.position.x + stats.fighterCenter.x, player.transform.position.y + stats.fighterCenter.y, player.transform.position.z + stats.fighterCenter.z);

        CapsuleCollider2D capsuleCollider = player.GetComponent<CapsuleCollider2D>();
        if (capsuleCollider == null)
        {
            capsuleCollider = player.AddComponent<CapsuleCollider2D>();
        }
        capsuleCollider.size = new Vector2(stats.width, stats.height);
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
        int fighter = UnityEngine.Random.Range(0, 3);
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

    // Helper method to add a component if it doesn't already exist
    private T AddComponentIfNotExists<T>(GameObject gameObject) where T : Component
    {
        T component = gameObject.GetComponent<T>();
        if (component == null)
        {
            component = gameObject.AddComponent<T>();
        }
        return component;
    }



}
