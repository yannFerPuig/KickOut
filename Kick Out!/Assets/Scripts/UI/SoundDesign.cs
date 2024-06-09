using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SoundDesign : MonoBehaviour
{
    [Header("--------- MUSIQUE -----------")]
    [SerializeField] AudioSource music;

    public Slider sliderMusic;
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


    public void ChangeVolume()
    {
        VolumeMusic = sliderMusic.value;
        music.volume = VolumeMusic;
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
