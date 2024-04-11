using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class RangedController : Controller
{

    [SerializeField] private float runDistance = 4.0f;
    private NavMeshAgent agent;

    // Start is called before the first frame update
    void Start()
    {
        gameObject.SetActive(false);
        agent = GetComponent<NavMeshAgent>();
        CloseArenaDoor.CloseDoor += ManageAgent;
    }

    private void ManageAgent()
    {
        gameObject.SetActive(true);
    }

    public override void Movement() {
        Debug.Log("I am getting called");
        if (gameObject.activeSelf && PlayerTracker.instance.player != null) {
            float distance = Vector3.Distance(transform.position, PlayerTracker.instance.player.transform.position);

            if (distance < runDistance) { 
                Vector3 dirToPlayer = transform.position - PlayerTracker.instance.player.transform.position;

                Vector3 newPosition = transform.position + dirToPlayer;

                agent.SetDestination(newPosition);
            }
        }
    }

    public override void Freeze()
    {
        Debug.Log("I should freeze ranged");
    }
}
