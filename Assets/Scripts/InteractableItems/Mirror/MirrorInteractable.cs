using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MirrorInteractable : MonoBehaviour, IInteractable
{

    public delegate void MirrorInteractedEvent();
    public static event MirrorInteractedEvent MirrorUsed;

    private Animator animator;

    private void Start()
    {
        animator = GetComponentInChildren<Animator>();
    }

    public void Interact()
    {
        if (animator != null)
        {
            animator.SetTrigger("Open");
            if (MirrorUsed != null)
            {
                MirrorUsed();
            }
        }
    }

    public string InteractionPrompt()
    {
        return "(E) Reflect";
    }

    public void CloseMenu() {
        if (animator != null) {
            animator.SetTrigger("Close");
        }
    }
}
