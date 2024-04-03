using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class AgentController : MonoBehaviour
{
    [SerializeField] private float stoppingDistance = 1.5f;
    private NavMeshAgent agent;

    private void Start()
    {

        gameObject.SetActive(false);
        CloseArenaDoor.CloseDoor += ManageAgent;
        agent = GetComponent<NavMeshAgent>();
    }

    private void ManageAgent() {
        gameObject.SetActive(true);
    }

    public void Freeze() {
        if (agent.hasPath)
        {
            agent.ResetPath();
        }
    }

    public void Movement() {
        if (gameObject.activeSelf && PlayerTracker.instance.player != null)
        {
            Vector3 targetPosition = PlayerTracker.instance.player.transform.position;
            float distanceToTarget = Vector3.Distance(targetPosition, transform.position);

            if (distanceToTarget <= stoppingDistance)
            {
                agent.ResetPath();
            }
            else
            {
                agent.SetDestination(targetPosition);
            }
        }
        else
        {
            if (agent.hasPath)
            {
                agent.ResetPath();
            }
        }
    }
}
