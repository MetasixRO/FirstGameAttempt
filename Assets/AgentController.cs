using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class AgentController : Controller
{
    [SerializeField] private float stoppingDistance = 1.5f;
    private NavMeshAgent agent;

    private void Start()
    {

        //gameObject.SetActive(false);
        //CloseArenaDoor.CloseDoor += ManageAgent;
        agent = GetComponent<NavMeshAgent>();
        newDeadState.RespawnPlayer += DestroyEnemy;
    }

    private void DestroyEnemy()
    {
        try
        {
            if (gameObject != null)
            {
                Destroy(gameObject);
            }
        }
        catch (MissingReferenceException)
        {
            //Debug.Log("yes");
        }
    }

    public new void ManageAgent() {
        gameObject.SetActive(true);
    }

    public override void Freeze() {
        agent.ResetPath();
        agent.isStopped = true;
        agent.velocity = Vector3.zero;
    }

    public override void Movement() {
        if (gameObject.activeSelf && PlayerTracker.instance.player != null)
        {
            Vector3 targetPosition = PlayerTracker.instance.player.transform.position;
            float distanceToTarget = Vector3.Distance(targetPosition, transform.position);

            if (agent.isStopped) {
                agent.isStopped = false;
            }

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
