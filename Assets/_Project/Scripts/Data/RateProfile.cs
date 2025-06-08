using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[Serializable]

public class RateProfile
{
    public bool IsRated;
    public int NumberShowRate;

    public RateProfile()
    {
        IsRated = false;
        NumberShowRate = 0;
    }
}
