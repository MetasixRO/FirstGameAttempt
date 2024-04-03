using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LookAtPlayer : MonoBehaviour
{

    private GameObject player;
    private AgentAnimations animations;


    void Start()
    {
        player = PlayerTracker.instance.player;
        animations = GetComponent<AgentAnimations>();
    }

    public void RotateToLookAtPlayer() {
        Vector3 rayDirection = transform.forward;
        Ray ray = new Ray(transform.position, rayDirection);
        RaycastHit hit;

        if (Physics.Raycast(ray, out hit))
        {
            if (hit.collider == null)
            {
                return;
            }

            if (hit.collider.gameObject == player)
            {
                animations.SetLookingAtPlayer(true);
            }
            else
            {
                animations.SetLookingAtPlayer(false);

                Vector3 directionToPlayer = (player.transform.position - transform.position).normalized;

                Quaternion lookLocation = Quaternion.LookRotation(new Vector3(directionToPlayer.x, 0, directionToPlayer.z));

                transform.rotation = Quaternion.Lerp(transform.rotation, lookLocation, 2.5f * Time.deltaTime);
            }
        }
    }
}
