using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponSelectorDoor : MonoBehaviour, IInteractable
{
    public delegate void ArmoryRoomEvent();
    public static event ArmoryRoomEvent EnteredArmory;

    public string prompt;
    private Animator animator;

    private void Start()
    {
        animator = GetComponent<Animator>();
        ReturnToLobby.BackToLobby += CloseDoor;
        DeadState.RespawnPlayer += CloseDoor;
    }

    public void Interact()
    {
        animator.SetTrigger("Open");
        if (EnteredArmory != null) {
            EnteredArmory();
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
    }
}
