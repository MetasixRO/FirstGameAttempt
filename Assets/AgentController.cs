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
        DoorOpener.OpenDoor += ManageAgent;
        agent = GetComponent<NavMeshAgent>();
    }

    private void ManageAgent() {
        gameObject.SetActive(true);
    }

    private void Update() {
        if (gameObject.activeSelf && PlayerTracker.instance != null && PlayerTracker.instance.player != null)
        {
            Vector3 targetPosition = PlayerTracker.instance.player.transform.position;
            float distanceToTarget = Vector3.Distance(targetPosition, transform.position);

            if (distanceToTarget <= stoppingDistance)
            {
                agent.ResetPath();
            }
            else {
                agent.SetDestination(targetPosition);
            }
        }
    }
}
