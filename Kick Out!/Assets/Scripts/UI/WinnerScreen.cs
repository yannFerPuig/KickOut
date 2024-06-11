using UnityEngine;
using UnityEngine.SceneManagement;

public class WinnerScreen : MonoBehaviour
{
    public void BackToMenu()
    {
        SceneManager.LoadScene("Menu");
    }
}