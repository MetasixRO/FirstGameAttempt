using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class DashController : Controller
{
    [SerializeField] private float totalDistance = 8.0f;
    private NavMeshAgent agent;

    private void Start()
    {
        //gameObject.SetActive(false);
        agent = GetComponent<NavMeshAgent>();
        //CloseArenaDoor.CloseDoor += ManageAgent;
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

    public new void ManageAgent()
    {
        gameObject.SetActive(true);
    }

    public override void Freeze()
    {
        agent.ResetPath();
        agent.isStopped = true;
    }

    public override void Movement()
    {
        if (gameObject.activeSelf && PlayerTracker.instance.player != null) {
            float distance = Vector3.Distance(transform.position, PlayerTracker.instance.player.transform.position);

            if (distance > totalDistance)
            {
                agent.isStopped = false;

                agent.SetDestination(PlayerTracker.instance.player.transform.position);
            }
            else
            {
                agent.ResetPath();
                agent.isStopped = true;
            }
        }
    }


}
