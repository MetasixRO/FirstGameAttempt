using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class BossController : Controller
{
    public delegate void BossMovementChecks();
    public static event BossMovementChecks CheckApproach;
    public static event BossMovementChecks CheckRun;
    public static event BossMovementChecks CheckReach;

    private bool shouldRun, shouldApproach, shouldReach;
    private bool canRun, canApproach;

    private bool isRunning, isApproaching;

    private NavMeshAgent agent;

    private void Start()
    {
        canRun = false;
        canApproach = false;

        isRunning = false;
        isApproaching = false;
        shouldRun = false;
        shouldReach = false;
        shouldApproach = false;

        agent = GetComponent<NavMeshAgent>();
        newDeadState.RespawnPlayer += DestroyEnemy;
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
            CheckMovement(distance);

            if (shouldRun)
            {
                agent.isStopped = false;

                Vector3 dirToPlayer = transform.position - PlayerTracker.instance.player.transform.position;

                Vector3 newPosition = transform.position + dirToPlayer;

                agent.SetDestination(newPosition);

                isRunning = true;
            }
            else if (shouldApproach)
            {
                agent.isStopped = false;

                agent.SetDestination(PlayerTracker.instance.player.transform.position);

                isApproaching = true;
            }
            else {
                agent.ResetPath();
                agent.isStopped = true;

                if (isRunning) {
                    isRunning = false;
                    shouldRun = false;
                    //StartCoroutine(DelayRunning());
                }

                if (isApproaching) {
                    isApproaching = false;
                    shouldApproach = false;
                    //StartCoroutine(DelayApproaching());
                }
            }
        }
    }

    private void CheckMovement(float distance) {
        if (distance < 5.0f && canRun) {
            shouldRun = true;
            canRun = false;
        }
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
}
