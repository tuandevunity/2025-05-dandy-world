using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public partial class DataManager
{
    public static SelfUtility SelfUtility { get; private set; }
    public static GamePlayUtility GamePlayUtility { get; private set; }
    public static SettingsUtility SettingsUtility { get; private set; }
    public static ShopUtility ShopUtility { get; private set; }
    public static EventUtility EventUtility { get; private set; }
    public  static RateUtility RateUtility { get; private set; }

    [RuntimeInitializeOnLoadMethod(RuntimeInitializeLoadType.BeforeSceneLoad)]
    protected static void OnInit()
    {
        
        SelfUtility = new SelfUtility("self_data");
        GamePlayUtility = new GamePlayUtility("gameplay_data");
        SettingsUtility = new SettingsUtility("settings_data");
        ShopUtility = new ShopUtility("shop_data");
        EventUtility = new EventUtility("event_data");
        RateUtility = new RateUtility("rate_data");
    }
}

