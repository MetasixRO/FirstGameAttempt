using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ArenaTeleporter : MonoBehaviour
{
    private static int generalID = 1;
    [SerializeField] int instanceID;

    private void Awake()
    {
        instanceID = generalID++;
    }

    private void Start()
    {
        LevelManager.EnterNextArena += EnterArena;
    }
    private void EnterArena(int id) {
        if (instanceID == id)
        {
            GameObject player = PlayerTracker.instance.player;
            CharacterController charController = player.GetComponent<CharacterController>();

            charController.enabled = false;
            player.transform.position = transform.position;
            charController.enabled = true;

            int doorID = 1;
            foreach (var door in GetComponentsInChildren<UnlockableDoor>()) {
                door.SetDoorInfo(instanceID, doorID);
                doorID++;
            }
        }
    }

    public static int GetGeneralID() {
        return generalID;
    }
}
