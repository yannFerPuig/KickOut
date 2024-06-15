using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class SoundDesign : MonoBehaviour
{
    [Header("--------- MUSIQUE -----------")]
    [SerializeField] AudioSource music;
    [SerializeField] AudioSource SFX;

    public Slider sliderMusic;
    public Slider sliderSFX;
    public AudioClip musicFight;
    public AudioClip musicEndScene;
    public static float VolumeMusic = 1;
    public static float VolumeSFX = 1;


    private static SoundDesign instance;


    void Awake()
    {
        // Check if an instance of SoundDesign already exists
        if (instance == null)
        {
            // If no, this is the instance
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else if (instance != this)
        {
            // If yes and it's not this instance, destroy this instance to prevent duplicates
            Destroy(gameObject);
            return;
        }

        // Subscribe to the sceneLoaded event
        SceneManager.sceneLoaded += OnSceneLoaded;
    }

    void OnDestroy()
    {
        // Unsubscribe from the event when the object is destroyed
        SceneManager.sceneLoaded -= OnSceneLoaded;
    }


    // Start is called before the first frame update
    void Start()
    {
        music.volume = VolumeMusic;
        music.clip = musicFight;
        music.Play();
        music.loop = true;
        music.volume = VolumeMusic;
    }

    public void ChangeVolume(string param)
    {
        if (param == "music")
        {
            VolumeMusic = sliderMusic.value;
            music.volume = VolumeMusic;
        }
        else if (param == "SFX")
        {
            VolumeSFX = sliderSFX.value;
            SFX.volume = VolumeSFX;
            PlaySFX(Resources.Load<AudioClip>("Sound/missedShot"));
        }
    }

    public void PlaySFX(AudioClip clip)
    {
        SFX.PlayOneShot(clip);
    }

    public void PutEndMusic()
    {
        music.clip = musicEndScene;
        music.Play();
        music.loop = true;
    }


    void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        // // Check if the loaded scene is the "Menu" scene
        // if (scene.name == "Menu")
        // {
        //     Destroy(gameObject);
        // }
    }
}