using System.Collections;
using System.Collections.Generic;
using System.Net.WebSockets;
using Cinemachine.Utility;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.Video;

public class EnemyMovement : TickBehaviour
{
    private NavMeshAgent agent;
    [SerializeField] private float speed = 2f; // Tốc độ di chuyển
    [SerializeField] private float squareSize = 7f; // Kích thước cạnh hình vuông

    private Vector3[] corners; // Mảng lưu tọa độ 4 góc
    private int currentCornerIndex = 0; // Chỉ số góc hiện tại
    private Vector3 targetPosition; // Vị trí mục tiêu
    private PlayerMovement player;
    [SerializeField] float checkDistance = 1.0f; // NAV
    [SerializeField] float limitDistance = 25f;

    [SerializeField] Transform pointCheck; // diem check cua nhan vat
    private Vector3 currentPointCheck;

    private bool waiting = false;
    public float waitTime = 0.5f;

    [SerializeField] Animator animator;

    [SerializeField] EnemyAnimBehavior enemyAnimBehavior;

    Vector3 startPos;

    [SerializeField] bool setPatrol = true;
    [SerializeField] DoorBoss doorBoos; 
    // Start is called before the first frame update
    void Start()
    {
        
        agent = GetComponent<NavMeshAgent>();
        InitEnemy();
        //Debug.Log(Vector3.Distance(currentPointCheck, ));
    }

    public void InitEnemy() {
        corners = new Vector3[4];
        startPos = transform.position;
        //Debug.Log(startPos);
        currentPointCheck = pointCheck.position;

        // Tính toán tọa độ các góc dựa trên kích thước hình vuông
        corners[0] = startPos;                           // Góc dưới trái
        corners[1] = startPos + new Vector3(squareSize, 0, 0); // Góc dưới phải
        corners[2] = startPos + new Vector3(squareSize, 0, squareSize); // Góc trên phải
        corners[3] = startPos + new Vector3(0, 0, squareSize); // Góc 
        agent.SetDestination(corners[0]);
    }

    public void ResetEnemy() {
        transform.position = startPos;
    }

    // Update is called once per frame
    void Update()
    {
        player = FindFirstObjectByType<PlayerMovement>();
        
        if (player != null) {
            float distanceToPlayer = Vector3.Distance(player.transform.position, currentPointCheck);
            bool isEnemyZoneLayer = Physics.Raycast(player.transform.position, Vector3.down, out RaycastHit _, 4f, LayerMask.GetMask(Preconsts.Layer_Enemy_Zone));
            Debug.DrawRay(player.transform.position, Vector3.down * 4f, isEnemyZoneLayer ? Color.green : Color.red);
            if (distanceToPlayer <= limitDistance && isEnemyZoneLayer) {
                currentPointCheck = transform.position;
                StartChasing();
            } else {
                currentPointCheck = pointCheck.position;
                Patrol();
            }
        } else  Patrol();
        
    }

    void Patrol() {
        if (!setPatrol) return;
        if (!waiting && !agent.pathPending && agent.remainingDistance < 0.1f)
        {
            
            StartCoroutine(GoToNextCorner());
            return;
        }
        
    }

    IEnumerator GoToNextCorner()
    {
        waiting = true;
        enemyAnimBehavior.SetEnemyState(EnemyAnimBehavior.EnemyState.Idle);
        yield return new WaitForSeconds(waitTime);
        enemyAnimBehavior.SetEnemyState(EnemyAnimBehavior.EnemyState.Walk);
        currentCornerIndex = (currentCornerIndex + 1) % 4;
        agent.SetDestination(corners[currentCornerIndex]);
        waiting = false;
        
    }

    void StartChasing()
    {
        enemyAnimBehavior.SetEnemyState(EnemyAnimBehavior.EnemyState.Walk);
        if (agent == null || player == null) return;

        agent.SetDestination(player.transform.position); // Ví dụ: di chuyển đến vị trí người chơi
        agent.isStopped = false;
    }

    // play
    void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag(Preconsts.Tag_Player)) {
            Debug.Log(player.isPlayerDie);
            if (player.isPlayerDie) return;
            Debug.Log("Die !");
            StopChasing();
            UIManager.ShowPanel<PanelDeath>(0.2f);
            StartCoroutine(player.Die(1f, 
                    () => 
                        {
                            agent.Warp(startPos);
                            enemyAnimBehavior.SetEnemyState(EnemyAnimBehavior.EnemyState.Idle);
                            currentPointCheck = pointCheck.position;
                            var enemy = GetComponent<Enemy>(); // boss
                            if (enemy != null) {
                                enemy.gun?.ResetGun();;
                                enemy.ResetHealth();
                            }
                            doorBoos?.ResetDoor();
                        })
                        );
        }
    }

    void StopChasing()
    {
        Debug.Log("Dừng đuổi theo.");
        if (agent == null || player == null) return;
        agent.isStopped = true;
    }

    void OnDrawGizmos()
    {
        
        if (corners == null || corners.Length != 4) return;
        
        Gizmos.color = Color.blue;
        for (int i = 0; i < 4; i++)
        {
            Gizmos.DrawLine(corners[i], corners[(i + 1) % 4]);
        }
    }
}
