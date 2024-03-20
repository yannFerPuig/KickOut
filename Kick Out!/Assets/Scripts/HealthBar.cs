using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HealthBar : MonoBehaviour
{
    //COMPONENTS
    public Slider slider;

    //Set the health bar to the max value
    public void SetMaxHealth(float health) 
    {
        slider.maxValue = health;
        slider.value = health;
    }

    //Whenever the health of the character is updated, the health bar is also updated
    public void SetHealth(float health) 
    {
        slider.value = health;
    }
}
