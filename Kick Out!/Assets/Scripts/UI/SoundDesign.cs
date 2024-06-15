using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class SoundDesign : MonoBehaviour
{
    [Header("--------- MUSIQUE -----------")]
    public AudioSource music;
    public AudioSource SFX;

    
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

    public void ChangeVolume(string param, float val)
    {
        if (param == "music")
        {
            VolumeMusic = val;
            music.volume = val;
            instance.music.volume = VolumeMusic;
        }
        else if (param == "SFX")
        {
            VolumeSFX = val;
            SFX.volume = val;
            instance.SFX.volume = VolumeSFX;
            PlaySFX(Resources.Load<AudioClip>("Sound/missedShot"));
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        // sliderMusic.maxValue = 100;
        // sliderSFX.maxValue = 100;

        music.volume = VolumeMusic;
        music.clip = musicFight;
        music.Play();
        music.loop = true;
        music.volume = VolumeMusic;
    }

    public void PlaySFX(AudioClip clip)
    {
        instance.SFX.PlayOneShot(clip);
        SFX.PlayOneShot(clip);
    }




    public void PutEndMusic()
    {
        music.clip = musicEndScene;
        music.Play();
        music.loop = true;
    }


    // Update is called once per frame
    void Update()
    {

    }

    void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        // Check if the loaded scene is the "Menu" scene
    }

}