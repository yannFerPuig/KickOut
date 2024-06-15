using UnityEngine;
using UnityEngine.SceneManagement;

public class FightersPresentation : MonoBehaviour
{
    public MainMenu menu;
    public void ChangeScene(string scene)
    {

        SceneManager.LoadSceneAsync(scene);
    }

    public void Tutorial()
    {
        menu = GameObject.FindGameObjectWithTag("MainCamera").GetComponent<MainMenu>();
        menu.gameMode = "tutorial";
        SceneManager.LoadScene("SoloCharacterSelection");
    }
}
