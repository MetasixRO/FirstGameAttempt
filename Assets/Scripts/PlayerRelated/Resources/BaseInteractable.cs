using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BaseInteractable : MonoBehaviour
{
    [SerializeField] private float closeAreaRadius = 2.5f;
    LayerMask arenaFloorMask;

    protected Vector3 CalculateSpawnPosition()
    {
        Vector3 randomPosition;
        GameObject player = PlayerTracker.instance.player;
        arenaFloorMask = LayerMask.GetMask("ArenaFloor");
        while (true)
        {
            randomPosition = player.transform.position + Random.insideUnitSphere * closeAreaRadius;

            randomPosition.y = player.transform.position.y;

            RaycastHit hit;
            Vector3 direction = (player.transform.position - randomPosition).normalized;
            if (Physics.Raycast(randomPosition, direction, out hit, closeAreaRadius, arenaFloorMask))
            {
                if (hit.point.y < randomPosition.y)
                {
                    continue;
                }
            }
            break;
        }

        return randomPosition;
    }
}
