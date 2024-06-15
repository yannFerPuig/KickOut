using UnityEngine;
using UnityEngine.SceneManagement;

public class WinnerScreen : MonoBehaviour
{
    public void BackToMenu()
    {
        SceneManager.LoadScene("Menu");
        GameObject.FindGameObjectWithTag("SoundManager").GetComponent<SoundDesign>().music.clip = Resources.Load<AudioClip>("Sound/KO-drill fivio-menu");
    }
}