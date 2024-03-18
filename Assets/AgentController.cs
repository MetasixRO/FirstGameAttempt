using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class AgentController : MonoBehaviour
{
    [SerializeField] private float stoppingDistance = 1.5f;
    private NavMeshAgent agent;
    private bool destinationIsCurrentPosition;

    private void Start()
    {
        destinationIsCurrentPosition = false;

        AgentAnimations.ManageDestination += ManageDestination;

        gameObject.SetActive(false);
        CloseArenaDoor.CloseDoor += ManageAgent;
        agent = GetComponent<NavMeshAgent>();
    }

    private void ManageAgent() {
        gameObject.SetActive(true);
    }

    private void Update() {
        if (gameObject.activeSelf && PlayerTracker.instance.player != null && !destinationIsCurrentPosition)
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
        else {
            if (agent.hasPath) {
                agent.ResetPath();
            }
        }
    }

    private void ManageDestination() {
        if (!destinationIsCurrentPosition)
        {
            destinationIsCurrentPosition = true;
        }
        else {
            destinationIsCurrentPosition = false;
        }
    }
}
