using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class RangedController : Controller
{

    [SerializeField] private float runDistance = 5.0f;
    [SerializeField] private float runDelay = 6.5f;
    private bool canRun, isRunning;
    private NavMeshAgent agent;

    // Start is called before the first frame update
    void Start()
    {
        //gameObject.SetActive(false);
        canRun = true;
        isRunning = false;
        agent = GetComponent<NavMeshAgent>();
        //CloseArenaDoor.CloseDoor += ManageAgent;
        newDeadState.RespawnPlayer += DestroyEnemy;
    }

    private void DestroyEnemy() {
        try
        {
            if (gameObject != null)
            {
                Destroy(gameObject);
            }
        }
        catch (MissingReferenceException) {
            //Debug.Log("yes");
        }
    }

    public new void ManageAgent()
    {
        gameObject.SetActive(true);
    }

    public override void Movement() {
        if (gameObject.activeSelf && PlayerTracker.instance.player != null && canRun)
        {
            float distance = Vector3.Distance(transform.position, PlayerTracker.instance.player.transform.position);

            if (distance < runDistance)
            {
                agent.isStopped = false;

                Vector3 dirToPlayer = transform.position - PlayerTracker.instance.player.transform.position;

                Vector3 newPosition = transform.position + dirToPlayer;

                agent.SetDestination(newPosition);

                isRunning = true;
            }
            else
            {
                agent.ResetPath();
                agent.isStopped = true;

                if (isRunning)
                {
                    isRunning = false;
                    canRun = false;
                    StartCoroutine(DelayRunning(runDelay));
                }
            }
        }
        
    }

    private IEnumerator DelayRunning(float delay) { 
        yield return new WaitForSeconds(delay);

        canRun = true;
    }

    public override void Freeze()
    {
        agent.ResetPath();
        agent.isStopped = true;
    }
}
