using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NextChamberDoor : MonoBehaviour, IInteractable
{
    public delegate void NewChamberEvent();
    public static event NewChamberEvent NewChamber;

    private Animator animator;

    private void Start()
    {
        animator = GetComponent<Animator>();
        ReachedChamber.ReachedNewChamber += RespawnDoor;
    }

    public void Interact()
    {
        if (NewChamber != null) {
            NewChamber();
        }

        gameObject.SetActive(false);

    }

    public string InteractionPrompt()
    {
        return "Advance to next chamber";
    }

    private void RespawnDoor() {
        gameObject.SetActive(true);
    }
}
