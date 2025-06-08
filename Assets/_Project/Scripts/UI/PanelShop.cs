using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.EventSystems;

public class PanelShop : BaseUI
{
    // Start is called before the first frame update
    public ShopItemSO[] shopItemSO;
    //public GameObject ShopTemplatePrefabs;
    //public GameObject contents;
    public ItemTemplate[] itemTemplate;
    public GameObject[] shopItems;
    public EventTrigger eventTriggerCloseShop;

    EventTrigger.Entry entryCloseShop = new EventTrigger.Entry
    {
        eventID = EventTriggerType.PointerDown
    };

    AudioManager _audio;

    protected override void Awake()
    {
        base.Awake();
        entryCloseShop.callback.AddListener((data) =>
        {
            OnClickTriggerCloseShop();
        });
        eventTriggerCloseShop.triggers.Add(entryCloseShop);
    }

    void Start()
    {
        _audio = AudioManager.Instance;
        for (int i = 0; i < shopItemSO.Length; i++)
        {
            shopItems[i].gameObject.SetActive(true);
        }
        LoadPanel();
    }


    void LoadPanel()
    {
        for (int i = 0; i < shopItemSO.Length; i++)
        {
            itemTemplate[i].Avatar.sprite = Resources.Load<Sprite>(shopItemSO[i].path); 
        }
    }

    void OnClickTriggerCloseShop()
    {
        _audio.SpawnAndPlay(transform, _audio.clickUISound, 0.5f);
        Hide();
    }
}
