using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class PanelSetting : BaseUI
{
    [Header("Event Trigger")]
    [SerializeField] EventTrigger eventTriggerClose;
    [SerializeField] EventTrigger eventTriggerVolume;
    [SerializeField] EventTrigger eventTriggerMusic;
    [SerializeField] EventTrigger eventTriggerResetGame;
    


    [Header("Component")]
    [SerializeField] Image volumeImage;
    [SerializeField] Image musicImage;

    [Header("Settings")]
    bool isMuteVolume = false;
    bool isMuteMusic = false;

    EventTrigger.Entry entryClose = new EventTrigger.Entry
    {
        eventID = EventTriggerType.PointerDown
    };

    EventTrigger.Entry entryVolume = new EventTrigger.Entry
    {
        eventID = EventTriggerType.PointerDown
    };

    EventTrigger.Entry entryMusic = new EventTrigger.Entry
    {
        eventID = EventTriggerType.PointerDown
    };

    EventTrigger.Entry entryResetGame = new EventTrigger.Entry
    {
        eventID = EventTriggerType.PointerDown
    };

    AudioManager _audio;

    protected override void Awake()
    {
        base.Awake();
        entryClose.callback.AddListener((data) => { OnClickTriggerClose(); });
        eventTriggerClose.triggers.Add(entryClose);

        entryVolume.callback.AddListener((data) => { OnClickToggleVolume(); });
        eventTriggerVolume.triggers.Add(entryVolume);

        entryMusic.callback.AddListener((data) => { OnClickToggleMusic(); });
        eventTriggerMusic.triggers.Add(entryMusic);

        entryResetGame.callback.AddListener((data) => { OnCickTriggerResetGame(); });
        eventTriggerResetGame.triggers.Add(entryResetGame);
    }
    // Start is called before the first frame update
    void Start()
    {
        _audio = AudioManager.Instance;
    }

    // Update is called once per frame

    void OnClickToggleVolume()
    {
        _audio.SpawnAndPlay(transform, _audio.clickUISound, 0.5f);
        isMuteVolume = !isMuteVolume;
        Sprite loadedSprite = isMuteVolume ? 
            Resources.Load<Sprite>(Preconsts.VolumeMuteIconPath) : 
            Resources.Load<Sprite>(Preconsts.VolumeIconPath);

        volumeImage.sprite = loadedSprite;
    }

    void OnClickToggleMusic()
    {
        _audio.SpawnAndPlay(transform, _audio.clickUISound, 0.5f);
        isMuteMusic = !isMuteMusic;
        Sprite loadedSprite = isMuteMusic ?
            Resources.Load<Sprite>(Preconsts.MuteIconPath) :
            Resources.Load<Sprite>(Preconsts.MusicIconPath);

        musicImage.sprite = loadedSprite;
    }

    void OnClickTriggerClose()
    {
        _audio.SpawnAndPlay(transform, _audio.clickUISound, 0.5f);
        Hide();
    }

    void OnCickTriggerResetGame() {
        _audio.SpawnAndPlay(transform, _audio.clickUISound, 0.5f);
        Debug.Log("rESETGAME");
        GameController.Instance.ResetGame();
    }
}
