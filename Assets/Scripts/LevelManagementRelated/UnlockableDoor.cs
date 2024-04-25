using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UnlockableDoor : MonoBehaviour
{
    private Material doorCurrentMaterial;

    private void Start()
    {
        doorCurrentMaterial = GetComponent<Renderer>().material;
    }

    private void Update()
    {
        if (Input.GetKeyDown("i")) {
            UnlockDoor();
        }
    }

    private void UnlockDoor() {
    }

    private void LockDoor() {
    }
}
