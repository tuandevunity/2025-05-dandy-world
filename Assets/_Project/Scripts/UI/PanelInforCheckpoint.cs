using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class PanelInforCheckpoint : BaseUI
{
    // Start is called before the first frame update
    [SerializeField] EventTrigger eventTriggerCloseUI;

    EventTrigger.Entry entryCloseUI = new EventTrigger.Entry
    {
        eventID = EventTriggerType.PointerDown
    };
    protected override void Awake()
    {
        base.Awake();
        entryCloseUI.callback.AddListener((data) => { OnClickTriggerCloseUI(); });
        

        eventTriggerCloseUI.triggers.Add(entryCloseUI);

    }

    private void OnClickTriggerCloseUI() {
        Hide();
    }




}
