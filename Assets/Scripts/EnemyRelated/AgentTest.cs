using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class AgentTest : MonoBehaviour
{
    private NavMeshAgent agent;
    // Start is called before the first frame update
    void Start()
    {
        agent = GetComponent<NavMeshAgent>();
    }

    // Update is called once per frame
    void Update()
    {
        Movement();
    }

    public void Movement()
    {
        if (gameObject.activeSelf && PlayerTracker.instance.player != null)
        {
            Vector3 targetPosition = PlayerTracker.instance.player.transform.position;
            float distanceToTarget = Vector3.Distance(targetPosition, transform.position);

            agent.SetDestination(targetPosition);
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
