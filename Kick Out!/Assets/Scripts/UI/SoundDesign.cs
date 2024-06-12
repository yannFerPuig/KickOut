using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

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
    {if (param == "music")
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


    // Update is called once per frame
    void Update()
    {

    }
}
