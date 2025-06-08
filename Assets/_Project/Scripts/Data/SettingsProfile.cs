using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class SettingsProfile
{
    public float Sensivity;
    public bool Music;
    public bool Sound;
    public bool Vibrate;
    public SettingsProfile()
    {
        Music = true;
        Sound = true;
        Vibrate = true;
        Sensivity = 0.5f;
    }
}
