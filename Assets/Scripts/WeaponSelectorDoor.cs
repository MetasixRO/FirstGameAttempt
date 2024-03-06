using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponSelectorDoor : MonoBehaviour, IInteractable
{
    public string prompt;
    private Animator animator;

    private void Start()
    {
        animator = GetComponent<Animator>();
    }

    public void Interact()
    {
        animator.Play("WeaponSelectorDoorOpen");
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
