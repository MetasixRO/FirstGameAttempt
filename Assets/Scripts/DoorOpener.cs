using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoorOpener : MonoBehaviour, IInteractable
{
    public delegate void DoorOpenerCallback();
    public static event DoorOpenerCallback OpenDoor;

    private bool usable;

    public string prompt;
    private Animator animator;

    private void Start()
    {
        animator = GetComponent<Animator>();

        usable = false;

        CloseArenaDoor.CloseDoor += CloseDoor;

        WeaponPrompt.ChangeWeapon += ActivateDoor;

        ReturnToLobby.BackToLobby += DeactivateDoor;

        DeadState.RespawnPlayer += ResetDoor;
    }

    public void Interact()
    {
        if (usable)
        {
            if (OpenDoor != null)
            {
                OpenDoor();
            }
            animator.SetTrigger("Open");
            //Parametru utilizat doar pentru verificare
            //Nu e utilizat nicaieri in animatorController
            animator.SetBool("isOpen", true);
        }
    }

    public string InteractionPrompt()
    {
        if (prompt != null)
        {
            return prompt;
        }
        else
            return "DefaultText";
    }

    private void CloseDoor() {
        animator.SetTrigger("Close");
        DeactivateDoor();
    }

    private void DeactivateDoor() {
        if (animator.GetBool("isOpen")) {
            animator.SetTrigger("Close");
            animator.SetBool("isOpen", false);
        }
        usable = false;
    }

    private void ResetDoor() {
        usable = true;
    }

    private void ActivateDoor(int weaponID) {
        ResetDoor();
    }
}
