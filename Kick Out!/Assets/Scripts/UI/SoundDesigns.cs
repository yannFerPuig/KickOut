using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class SoundDesigns : MonoBehaviour
{
    [Header("--------- MUSIQUE -----------")]
    [SerializeField] AudioSource music;

    public AudioClip musicFight;
    public AudioClip musicEndScene;
    // Start is called before the first frame update
    void Start()
    {
        music.clip = musicFight;
        music.Play();
        music.loop = true;
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
