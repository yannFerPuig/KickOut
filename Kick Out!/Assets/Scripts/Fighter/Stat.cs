using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class Stat
{
    [SerializeField]
    private float baseValue;

    public float GetValue() 
    {
        return baseValue;
    }

    public void SetValue(float value)
    {
        baseValue = value;
    }
}
