using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MainMenu : MonoBehaviour
{
    public GameObject img;
    public List<Sprite> sprites;

    
    public void ChangeScene(string scene)
    {
        SceneManager.LoadSceneAsync(scene);
    }

    public void NextSprite(int index)
    {
        img.GetComponent<Image>().sprite = sprites[index];
    }
}