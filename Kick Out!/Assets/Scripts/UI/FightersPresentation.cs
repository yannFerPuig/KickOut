using UnityEngine;
using UnityEngine.SceneManagement;

public class FightersPresentation : MonoBehaviour
{
    public void ChangeScene(string scene)
    {
        SceneManager.LoadSceneAsync(scene);
    }
}
