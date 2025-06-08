using System.Collections;
using JetBrains.Annotations;
using UnityEngine;
using UnityEngine.AI;

public class MapController : TickBehaviour
{
    public Transform Checkpoint;

    public Transform[] Checkpoints;

    [SerializeField] GameObject NPCPrefab;

    private float lastSpawnTime = 0f;
    public float spawnInterval;
    [SerializeField] bool SpawnNPC = true;

    protected override void Awake()
    {
        base.Awake();
        int idCheckpoint = DataManager.GamePlayUtility.Profile.CheckPointCurrent();  /// chỉ số mảng 
        Debug.Log("id" + idCheckpoint);
        Checkpoint = FindCheckpointByID(idCheckpoint).transform;
    }

    private void Start() {
        
    }

    void Update()
    {
        if (!SpawnNPC) return; 
        spawnInterval = Random.Range(6f,10f);
        if (Time.time - lastSpawnTime >= spawnInterval)
        {
            lastSpawnTime = Time.time;
            InitNPC();
        }
    }


    private void InitNPC() {
        if (NPCPrefab == null) return;
        SetPosNPCPrefab(GetCurrentCheckPointPosition());
        GameObject NPC = Instantiate(NPCPrefab);
        var agent = NPC.GetComponent<NavMeshAgent>();
        Debug.Log("agent " + agent + GetCheckpointPositionNext());
        agent.SetDestination(GetCheckpointPositionNext());
    }

    private void SetPosNPCPrefab(Vector3 pos) {
        NPCPrefab.transform.position = pos;
    }

    private Vector3 GetCurrentCheckPointPosition() {
        int idCheckpoint = DataManager.GamePlayUtility.Profile.CheckPointCurrent();

        if (idCheckpoint == -1) return Checkpoint.position; // check point ban dau;
        return Checkpoints[idCheckpoint - 1].position;

    }

    private Vector3 GetCheckpointPositionNext() {
        int idCheckpoint = DataManager.GamePlayUtility.Profile.CheckPointCurrent();
        if (idCheckpoint == -1) return Checkpoints[0].position;
        return Checkpoints[idCheckpoint].position;
    }

    public Transform FindCheckpointByID(int id)
    {
        Checkpoint[] allCheckpoints = FindObjectsOfType<Checkpoint>();

        foreach (var checkpoint in allCheckpoints)
        {
            if (checkpoint.ID == id)
            {
                return checkpoint.transform;
            }
        }

        Debug.LogWarning($"Checkpoint with ID {id} not found!");
        return null;
    }

    [Button("Change", EButtonEnableMode.Always)]
    private void ChangeCheckpoint(int ID) {
        DataManager.GamePlayUtility.Profile.OnChangedCheckPointInMap(ID, () => {
                Debug.Log("Checkpoint updated!");
                DataManager.GamePlayUtility.Save(); // Lưu luôn sau khi chạm
            });
    }

}
