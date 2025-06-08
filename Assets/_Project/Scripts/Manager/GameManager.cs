using System.Collections.Generic;
using System.Linq;
using UnityEngine;
public class GameManager : Singleton<GameManager>
{
    [SerializeField] int MapMax;
    protected override void Awake()
    {
        base.Awake();
        DontDestroyOnLoad(this.gameObject);
        LoadData();
    }

    void LoadData()
    {
        LoadGamePlayData();
    }

    void LoadGamePlayData()
    {
        var dataGameplay = DataManager.GamePlayUtility.Profile;
        var listCheckPoint = dataGameplay.ListCheckPointEachMap;

        for(int i = 0; i < MapMax; i++)
        {
            if(i >= listCheckPoint.Count)
            {
                listCheckPoint.Add(-1);
            }
        }

        while(listCheckPoint.Count > MapMax)
        {
            listCheckPoint.RemoveAt(listCheckPoint.Count - 1);
        }
    }
}