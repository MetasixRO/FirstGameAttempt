using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class DashController : Controller
{
    [SerializeField] private float totalDistance = 7.5f;
    private NavMeshAgent agent;

    private LayerMask layerToIgnore;
    private int layerMask;

    private void Start()
    {
        totalDistance = 7.5f;
        layerToIgnore = LayerMask.GetMask("Enemy");
        layerMask = Physics.DefaultRaycastLayers & ~layerToIgnore.value;
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
        agent.velocity = Vector3.zero;
    }

    public override void Movement()
    {
        if (gameObject.activeSelf && PlayerTracker.instance.player != null) {
            float distance = Vector3.Distance(transform.position, PlayerTracker.instance.player.transform.position);

            if (distance > totalDistance || !CheckCanSeePlayer(distance))
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


}
