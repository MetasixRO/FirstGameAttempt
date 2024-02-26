using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyEntityManager : MonoBehaviour
{
    void Start()
    {
        DoorOpener.OpenDoor += ActivateEnemy;
        gameObject.SetActive(false);
    }

    private void ActivateEnemy() { 
        gameObject.SetActive(true);
        DoorOpener.OpenDoor -= ActivateEnemy;
    }

}
