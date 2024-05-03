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

    
    public void ChangeScene(string scene)
    {
        SceneManager.LoadSceneAsync(scene);
    }

    public void Solo() 
    {
        gameMode = "solo";
    }

    public void Duel()
    {
        gameMode = "duel";
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