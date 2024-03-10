using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Teleporter : MonoBehaviour
{
    GameObject player;

    private void Start()
    {
        player = PlayerTracker.instance.player;
        DeadState.RespawnPlayer += RespawnPlayer;
    }

    private void RespawnPlayer() { 
        player.transform.position = transform.position;
        player.GetComponent<Collider>().enabled = true;
        player.GetComponent<Animator>().SetTrigger("Reset");
    }
}
