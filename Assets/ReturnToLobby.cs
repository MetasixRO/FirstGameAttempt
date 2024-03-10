using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ReturnToLobby : MonoBehaviour
{
    public delegate void ReturnToLobbyEvent();
    public static event ReturnToLobbyEvent BackToLobby;

    private Collider thisCollider;

    private void Start()
    {
        thisCollider = GetComponent<Collider>();

        thisCollider.enabled = false;

        WeaponPrompt.ChangeWeapon += ActivatePoint;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player")) {
            if (BackToLobby != null) {
                BackToLobby();
            }
            thisCollider.enabled = false;
        }
    }

    private void ActivatePoint(int weaponID) {
        thisCollider.enabled = true;
    }
}
