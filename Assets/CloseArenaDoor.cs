using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CloseArenaDoor : MonoBehaviour
{
    public delegate void CloseArenaDoorEvent();
    public static event CloseArenaDoorEvent CloseDoor;

    private void Start()
    {
        DeadState.RespawnPlayer += ResetPoint;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player") && CloseDoor != null){
            CloseDoor();
            GetComponent<Collider>().enabled = false;
        }
    }

    private void ResetPoint()
    {
        GetComponent<Collider>().enabled = true;
    }
}
