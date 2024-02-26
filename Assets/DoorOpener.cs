using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoorOpener : MonoBehaviour, IInteractable
{
    public delegate void DoorOpenerCallback();
    public static event DoorOpenerCallback OpenDoor;

    public string prompt;
    private Animator animator;

    private void Start()
    {
        animator = GetComponent<Animator>();
    }

    public void Interact()
    {
        if (OpenDoor != null) {
            OpenDoor();
        }
        animator.Play("OpenDoor");
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
}
