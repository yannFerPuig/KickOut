using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Option : MonoBehaviour
{

    public SoundDesign soundManager;
    public Slider sliderMusic;
    public Slider sliderSFX;

    // Start is called before the first frame update
    void Start()
    {
        soundManager = GameObject.FindGameObjectWithTag("SoundManager").GetComponent<SoundDesign>();
    }


    public void ChangeVolume(string param)
    {
        if (param == "music")
        {
            soundManager.ChangeVolume(param, sliderMusic.value);
        }
        else if (param == "SFX")
        {
            soundManager.ChangeVolume(param, sliderSFX.value);
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
