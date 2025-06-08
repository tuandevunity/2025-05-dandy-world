using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PanelTutorialMove : BaseUI
{
    // Start is called before the first frame update
    protected override void Awake()
    {
        base.Awake();
    }

    protected override void OnEnable()
    {
        base.OnEnable();
    }

    public override void OnShow()
    {
        Group.alpha = 1;
        Group.blocksRaycasts = false;
        gameObject.SetActive(true);
    }
}
