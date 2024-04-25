using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ReachedChamber : MonoBehaviour
{
    public delegate void ReachedChamberEvent();
    public static event ReachedChamberEvent ReachedNewChamber;

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player")) {
            ReachedNewChamber();
            gameObject.SetActive(false);
        }
    }
}
