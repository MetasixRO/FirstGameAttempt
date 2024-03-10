using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Look : MonoBehaviour
{

    GameObject player;

    private void Start()
    {
        player = PlayerTracker.instance.player;
    }

    // Update is called once per frame
    void Update()
    {
        LookAtPlayer();
    }

    private void LookAtPlayer()
    {
        Vector3 directionToPlayer = (player.transform.position - transform.position).normalized;

        Quaternion lookLocation = Quaternion.LookRotation(new Vector3(directionToPlayer.x, 0, directionToPlayer.z));

        transform.rotation = Quaternion.Lerp(transform.rotation, lookLocation, 1.0f * Time.deltaTime);
    }
}
