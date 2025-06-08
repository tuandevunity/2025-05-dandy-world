using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Loading : TickBehaviour
{
    protected override void Awake()
    {
        Debug.Log("HIHI");
        base.Awake();
        UIManager.ShowPanel<PanelLoading>(0);
        var audio = AudioManager.Instance;
        
    }
}
