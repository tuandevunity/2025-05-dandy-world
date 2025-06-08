using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class NPCMovement : MonoBehaviour
{
    // Start is called before the first frame update
    private NavMeshAgent agent;
    void Start()
    {
        agent = GetComponent<NavMeshAgent>();
        if (!agent.isOnNavMesh) {
            Debug.LogError("agent is not placed on the navmesh");
            return;
        }
    }

    void Update()
    {
        if (agent.remainingDistance <= agent.stoppingDistance && !agent.pathPending)
        {
            Debug.Log("da den dich");
            Destroy(gameObject);
        }
    }
}
