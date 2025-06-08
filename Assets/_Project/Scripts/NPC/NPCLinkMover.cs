using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class NPCLinkMover : MonoBehaviour
{
    [SerializeField] NavMeshAgent agent;

    [SerializeField] OffMeshLinkMoveMethod m_Method = OffMeshLinkMoveMethod.Parabola;
    IEnumerator Start()
    {
        //m_Animator = GetComponent<Animator>();
        agent = GetComponent<NavMeshAgent>();

        agent.autoTraverseOffMeshLink = false;
        while (true)
        {
            if (agent.isOnOffMeshLink)
            {
                if (m_Method == OffMeshLinkMoveMethod.NormalSpeed)
                    yield return StartCoroutine(NormalSpeed(agent));
                else if (m_Method == OffMeshLinkMoveMethod.Parabola)
                    yield return StartCoroutine(Parabola(agent, 2.5f, 0.5f));
                // else if (m_Method == OffMeshLinkMoveMethod.Curve)
                //     yield return StartCoroutine(Curve(agent, 0.5f));

                agent.CompleteOffMeshLink();
            }

            yield return null;
        }
    }

    // Update is called once per frame
    IEnumerator Parabola(NavMeshAgent agent, float height, float duration) {
        OffMeshLinkData data = agent.currentOffMeshLinkData;
        Vector3 startPos = agent.transform.position;
        Vector3 endPos = data.endPos + Vector3.up * agent.baseOffset;
        float normalizedTime = 0.0f;
        while (normalizedTime < 1.0f) {
            // anim
            float yOffset = height * 4.0f * (normalizedTime - normalizedTime * normalizedTime);
            agent.transform.position = Vector3.Lerp(startPos, endPos, normalizedTime) + yOffset * Vector3.up;
            normalizedTime += Time.deltaTime / duration;
            yield return null;
        }
    }

    IEnumerator NormalSpeed(NavMeshAgent agent) {
        OffMeshLinkData data = agent.currentOffMeshLinkData;
        Vector3 startPos = agent.transform.position;
        Vector3 endPos = data.endPos + Vector3.up * agent.baseOffset;

        // Tạm tắt điều khiển NavMesh
        agent.isStopped = true;

        while (Vector3.Distance(agent.transform.position, endPos) > 0.05f) {
            agent.transform.position = Vector3.MoveTowards(agent.transform.position, endPos, agent.speed * Time.deltaTime);
            yield return null;
        }

        agent.transform.position = endPos; // Snap chính xác cuối điểm
        agent.CompleteOffMeshLink();       // Báo là đã xong link
        agent.isStopped = false;
    }

}

enum OffMeshLinkMoveMethod {
    Parabola,
    NormalSpeed
}

