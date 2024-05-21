using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class BossController : Controller
{
    private int layerMask;
    private LayerMask firstLayerToIgnore;
    private LayerMask secondLayerToIgnore;


    private bool shouldApproach, shouldReach;
    private bool decisionMade;

    private NavMeshAgent agent;

    private void Start()
    {
        firstLayerToIgnore = LayerMask.GetMask("Enemy");
        secondLayerToIgnore = LayerMask.GetMask("Boss");
        layerMask = Physics.DefaultRaycastLayers & ~firstLayerToIgnore.value & ~secondLayerToIgnore.value;

        decisionMade = false;

        shouldReach = false;
        shouldApproach = false;

        agent = GetComponent<NavMeshAgent>();
        newDeadState.RespawnPlayer += DestroyEnemy;
    }

    public override void Freeze()
    {
        agent.ResetPath();
        agent.isStopped = true;
        agent.velocity = Vector3.zero;
    }

    public override void Movement()
    {
        bool shouldMove = false;
        float threshold = 0f;
        bool checkVisibility = false;

        if (gameObject.activeSelf && PlayerTracker.instance.player != null) {
            float distance = Vector3.Distance(transform.position, PlayerTracker.instance.player.transform.position);
            if (!decisionMade)
            {
                CheckMovement(distance);
            }

            if (shouldReach)
            {
                shouldMove = true;
                threshold = 1.2f;
            }
            else if (shouldApproach)
            {
                shouldMove = true;
                threshold = 5.0f;
                checkVisibility = true;
            }
            else {
                shouldMove = !CheckCanSeePlayer(distance);
                checkVisibility = true;
            }

            if (shouldMove && (distance > threshold || (checkVisibility && !CheckCanSeePlayer(distance))))
            {
                agent.isStopped = false;
                agent.SetDestination(PlayerTracker.instance.player.transform.position);
            }
            else {
                agent.ResetPath();
                agent.isStopped = true;
            }
        }
    }

    private void CheckMovement(float distance) {
        int nextMove = Random.Range(1, 4);
        // 1 : Melee
        // 2 : Area
        // 3 : Range
        Debug.Log("Decision made : " + nextMove);
        switch (nextMove)
        {
            case 1:
                shouldReach = true;
                shouldApproach = false;
                decisionMade = true;
                break;
            case 2:
                shouldApproach = true;
                shouldReach = false;
                decisionMade = true;
                break;
            case 3:
                shouldReach = false;
                shouldApproach = false;
                decisionMade = true;
                break;
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

    private bool CheckCanSeePlayer(float maxDistance)
    {
        Vector3 direction = (PlayerTracker.instance.player.transform.position - gameObject.transform.position).normalized;

        Ray ray = new Ray(gameObject.transform.position, direction);

        RaycastHit hit;
        if (Physics.Raycast(ray, out hit, maxDistance, layerMask))
        {
            if (hit.collider.gameObject == PlayerTracker.instance.player)
            {
                return true;
            }
        }
        else
        {
            return true;
        }
        return false;
    }

    private void AttackDone() {
        shouldApproach = false;
        shouldReach = false;
        decisionMade = false;
    }
}
