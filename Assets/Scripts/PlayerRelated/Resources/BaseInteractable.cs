using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BaseInteractable : MonoBehaviour
{
    [SerializeField] private float closeAreaRadius = 5f;

    protected Vector3 CalculateSpawnPosition()
    {
        Vector3 randomPosition;
        GameObject player = PlayerTracker.instance.player;
        while (true)
        {
            randomPosition = player.transform.position + Random.insideUnitSphere * closeAreaRadius;

            randomPosition.y = Mathf.Max(randomPosition.y, 0);

            RaycastHit hit;
            Vector3 direction = (player.transform.position - randomPosition).normalized;
            if (Physics.Raycast(randomPosition, direction, out hit, closeAreaRadius))
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
