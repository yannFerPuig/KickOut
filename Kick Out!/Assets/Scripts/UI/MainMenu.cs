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
            LoadFighter();
        }
    }

    void LoadFighter()
    {
        GameObject player = GameObject.FindGameObjectWithTag("Player");
        player.transform.position = new Vector3(-8f, -2f, 0f);

        Animator animator = player.gameObject.AddComponent(typeof(Animator)) as Animator;

        switch (fighterSelected)
        {
            case "Carmen":
                player.GetComponent<SpriteRenderer>().sprite = Resources.Load<Sprite>("BaseSprites/Carmen");

                player.gameObject.AddComponent(typeof(CarmenStats));
                RuntimeAnimatorController runtimeAnimatorController = Resources.Load<RuntimeAnimatorController>("Animation/Carmen/Carmen");
                animator.runtimeAnimatorController = runtimeAnimatorController;

                CarmenStats statsCarmen = player.GetComponent<CarmenStats>();
                statsCarmen.Initialize();

                statsCarmen.attackPoint.transform.position = new Vector3(player.transform.position.x + 0.25f, player.transform.position.y + 1.1f, statsCarmen.attackPointPos.z); 
                statsCarmen.groundCheck.transform.position = new Vector3(player.transform.position.x, player.transform.position.y - 1.8f, player.transform.position.z);

                CapsuleCollider2D capsuleCollider2D = player.GetComponent<CapsuleCollider2D>();
                capsuleCollider2D.size = new Vector2(statsCarmen.width, statsCarmen.height);

                PlayerAttack playerAttack = player.GetComponent<PlayerAttack>();
                playerAttack.punch = Resources.Load<AnimationClip>("Animation/Carmen/carmenPunch");
                playerAttack.special = Resources.Load<AnimationClip>("Animation/Carmen/carmenSpecial");

                break;

            case "Louis":
                player.GetComponent<SpriteRenderer>().sprite = Resources.Load<Sprite>("BaseSprites/Louis");

                player.gameObject.AddComponent(typeof(LouisStats));
                RuntimeAnimatorController louisController = Resources.Load<RuntimeAnimatorController>("Animation/Louis/Louis");
                animator.runtimeAnimatorController = louisController;

                LouisStats statsLouis = player.GetComponent<LouisStats>();
                statsLouis.Initialize();

                player.transform.position = new Vector3(-8.5f, -2f, 0f);
                statsLouis.attackPoint.transform.position = new Vector3(player.transform.position.x + 0.25f, player.transform.position.y + 1.1f, statsLouis.attackPointPos.z); 
                statsLouis.groundCheck.transform.position = new Vector3(player.transform.position.x, player.transform.position.y - 1.8f, player.transform.position.z);

                CapsuleCollider2D colliderLouis = player.GetComponent<CapsuleCollider2D>();
                colliderLouis.size = new Vector2(statsLouis.width, statsLouis.height);

                PlayerAttack louisAttack = player.GetComponent<PlayerAttack>();
                louisAttack.punch = Resources.Load<AnimationClip>("Animation/Louis/Attack");
                louisAttack.special = Resources.Load<AnimationClip>("Animation/Louis/Special");

                break;

            case "BobUn":
                player.GetComponent<SpriteRenderer>().sprite = Resources.Load<Sprite>("BaseSprites/BobUn");

                player.gameObject.AddComponent(typeof(BobUnStats));
                RuntimeAnimatorController bobUnController = Resources.Load<RuntimeAnimatorController>("Animation/BobUn/BobUn");
                animator.runtimeAnimatorController = bobUnController;

                BobUnStats statsBobUn = player.GetComponent<BobUnStats>();
                statsBobUn.Initialize();

                player.transform.position = new Vector3(-8.5f, -2f, 0f);
                statsBobUn.attackPoint.transform.position = new Vector3(player.transform.position.x + 0.25f, player.transform.position.y + 1.1f, statsBobUn.attackPointPos.z); 
                statsBobUn.groundCheck.transform.position = new Vector3(player.transform.position.x, player.transform.position.y - 1.8f, player.transform.position.z);

                CapsuleCollider2D colliderBobUn = player.GetComponent<CapsuleCollider2D>();
                colliderBobUn.size = new Vector2(statsBobUn.width, statsBobUn.height);

                PlayerAttack bobUnAttack = player.GetComponent<PlayerAttack>();
                bobUnAttack.punch = Resources.Load<AnimationClip>("Animation/BobUn/bobUnAttack");
                bobUnAttack.special = Resources.Load<AnimationClip>("Animation/BobUn/bobUnSpecial");

                break;
        }
    }

    public void SetFighterImage()
    {
        
    }
}