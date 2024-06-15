using UnityEngine;
using UnityEngine.SceneManagement;

public class FightersPresentation : MonoBehaviour
{
    public MainMenu menu;


    private void Start()
    {
        menu = GameObject.FindGameObjectWithTag("MainCamera").GetComponent<MainMenu>();
    }

    public void Tutorial()
    {
        menu.gameMode = "tutorial";
        SceneManager.LoadScene("SoloCharacterSelection");
    }

    public void ChangeScene(string scene)
    {
        if (scene == "Menu" && (SceneManager.GetActiveScene().name == "SoloCharacterSelection" || SceneManager.GetActiveScene().name == "MultiCharacterSelection"))
            menu.ChangeMusic(Resources.Load<AudioClip>("Sound/KO-drill fivio-menu"));
        SceneManager.LoadSceneAsync(scene);
    }
}
