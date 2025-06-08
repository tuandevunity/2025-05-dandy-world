using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class PanelUI : BaseUI
{
    // Start is called before the first frame update
    [SerializeField] EventTrigger eventTriggerSetting;
    [SerializeField] EventTrigger eventTriggerShop;

    EventTrigger.Entry entrySetting = new EventTrigger.Entry
    {
        eventID = EventTriggerType.PointerDown
    };

    EventTrigger.Entry entryShop = new EventTrigger.Entry
    {
        eventID = EventTriggerType.PointerDown
    };

    AudioManager _audio;
    protected override void Awake()
    {
        base.Awake();
        entrySetting.callback.AddListener((data) =>
        {
            OnClickTriggerSetting();
        });
        eventTriggerSetting.triggers.Add(entrySetting);

        entryShop.callback.AddListener((data) =>
        {
            OnClickTriggerShop();
        });
        eventTriggerShop.triggers.Add(entryShop);

    }

    void Start()
    {
        _audio = AudioManager.Instance;
    }

    protected override void OnEnable()
    {
        base.OnEnable();
    }

    public override void OnShow()
    {
        Group.alpha = 1;
        Group.blocksRaycasts = true;
        gameObject.SetActive(true);
    }
    void OnClickTriggerSetting()
    {
        _audio.SpawnAndPlay(transform, _audio.clickUISound, 0.5f);
        UIManager.ShowPanel<PanelSetting>(0);
    }

    void OnClickTriggerShop()
    {
        _audio.SpawnAndPlay(transform, _audio.clickUISound, 0.5f);
        UIManager.ShowPanel<PanelShop>(0);
    }
}
