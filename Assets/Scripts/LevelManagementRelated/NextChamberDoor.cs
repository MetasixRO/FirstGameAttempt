using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NextChamberDoor : MonoBehaviour, IInteractable
{
    public delegate void NewChamberEvent(NextChamberDoor instance);
    public static event NewChamberEvent NewChamber;

    private Animator animator;

    private bool isInteractable;

    private void Start()
    {
        isInteractable = false;
    }

    public void Interact()
    {
        if (isInteractable)
        {
            NewChamber?.Invoke(this);
        }
    }

    public string InteractionPrompt()
    {
        return "Advance to next chamber";
    }

    public void SetInteractable(bool value) {
        isInteractable = value;
    }
}
