using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Test : MonoBehaviour
{
    private void Start()
    {
        Debug.Log("Is this script even working?");

        Collider collider = GetComponent<Collider>();

        if (collider != null) {
            Debug.Log("Has collider");
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        Debug.Log("Trigger : " + other.tag);
        Debug.Log("\n\n\n");
    }

    private void OnCollisionEnter(Collision collision)
    {
        Debug.Log("Collision : " + collision.collider.tag);
        Debug.Log("\n\n\n");
    }
}
