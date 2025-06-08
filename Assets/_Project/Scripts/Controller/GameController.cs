using System;
using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameController : Singleton<GameController>
{
    public Transform checkpointTest;
    [SerializeField] GameObject playerPrefab;
    public GameState GameState;
    public MainCamController MainCamController;
    public MapController MapController { get; private set; }
    public PlayerController PlayerController { get; private set; }
    public PlayerMovement PlayerMovement { get; private set; }
    public CamPlayer CamPlayer { get; private set; }

    protected override void Awake()
    {
        base.Awake();
        Debug.Log("game controller");
        DontDestroyOnLoad(gameObject);
        Application.targetFrameRate = 60;

    }
    protected override void OnDisable()
    {
        Debug.Log("On disable");
        base.OnDisable();
        SetPosPrefabPlayer(Vector3.zero);
    }
    void SetPosPrefabPlayer(Vector3 posSpawn)
    {
        playerPrefab.transform.position = posSpawn;
    }

    private void InitUI(bool showTutor = true) {
        UIManager.ShowPanel<PanelGamePlay>(0);
        if (showTutor) {
            UIManager.ShowPanel<PanelTutorialMove>(0);
            UIManager.ShowPanel<PanelTutorialRotate>(0);
        }
        UIManager.ShowPanel<PanelUI>(0);
    }
    public void LoadingGame(Action complete = null)
    {
        var dataGamePlay = DataManager.GamePlayUtility.Profile;
        StartCoroutine(LoadSceneAsync(() =>
        {
            complete?.Invoke();
            InitUI();
            SetGameState(GameState.Playing);
        }));
    }
    public void ResetGame()
    {
        ResetMap();
        var dataGamePlay = DataManager.GamePlayUtility.Profile;
        UIManager.HideAllPanel();
        UIManager.ShowPanel<PanelLoadScene>(0).OnShow(StartCoroutine(IELoadScenePlayGame()), complete: () =>
        {
            InitUI(showTutor: false);
            SetGameState(GameState.Playing);
        });
    }
    public void ResetMap()
    {
        var dataGamePlay = DataManager.GamePlayUtility.Profile;
        DataManager.SelfUtility.Profile.Spoon = false;
        dataGamePlay.ResetMap();
    }
    public void InitPlayer(Transform transSpawnPlayer, bool setPosPlayer = true) // Khoi tao nguoi choi
    {
        var spawnPlayer = transSpawnPlayer;
        SetPosPrefabPlayer(setPosPlayer ? transSpawnPlayer.position - transSpawnPlayer.forward * 0.5f : transSpawnPlayer.position);
        if(PlayerController != null) Destroy(PlayerController.gameObject);
        var player = Instantiate(playerPrefab);
        CamPlayer = player.GetComponent<CamPlayer>();
        MainCamController.SetFollowAndTimeChangeCam(CamPlayer.GetCamTarget(), 0.5f);
        PlayerController = player.GetComponent<PlayerController>();
        PlayerMovement = player.GetComponent<PlayerMovement>();
        PlayerMovement.ResetPlayer(spawnPlayer.eulerAngles.y);
        CamPlayer.SetRotCamFirstGame(spawnPlayer.eulerAngles.y);
    }

    IEnumerator LoadSceneAsync(Action complete = null)
    {
        AsyncOperation operation = SceneManager.LoadSceneAsync("Map" + DataManager.GamePlayUtility.Profile.Map);
        while (!operation.isDone)
        {
            float progressValue = Mathf.Clamp01(operation.progress / 0.9f);
            UIManager.Panel<PanelLoading>().OnProgressPercent(progressValue);
            yield return null;
        }
        MapController = FindObjectOfType<MapController>();
        InitPlayer(CheckPointCurrent());
        //Debug.Log(CheckPointCurrent().position); // load checkpoint ban dau
        complete?.Invoke();
    }
    IEnumerator IELoadScenePlayGame()
    {
        SceneManager.LoadScene("Map" + DataManager.GamePlayUtility.Profile.Map);
        yield return null;
        MapController = FindObjectOfType<MapController>();
        InitPlayer(CheckPointCurrent());
    }
    Transform CheckPointCurrent(bool increaseCheckPoint = false)
    {
        var dataGamePlay = DataManager.GamePlayUtility.Profile;
        if(increaseCheckPoint) dataGamePlay.IncreseCheckPointInMap();
        return MapController.Checkpoint;
    }
    public void SetGameState(GameState gameState) => GameState = gameState;
}
public enum GameState
{
    None,
    Playing,
    Pause,
}
