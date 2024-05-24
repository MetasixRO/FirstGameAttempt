using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ManageDialogueBox : MonoBehaviour,IInteractable
{
    public delegate void DialogueEvent();
    public static event DialogueEvent dialogueTriggered;
    public static event DialogueEvent giftDialogueTriggered;
    public static event DialogueEvent checkEnoughAmbrosia;

    private bool canGift;
    private bool canInteract;

    private void Start()
    {
        canGift = false;
        canInteract = true;
        ResourceManager.CheckAmbrosiaResult += SetCanGift;
    }

    public void Interact()
    {
        if (canInteract)
        {
            canInteract = false;
            if (dialogueTriggered != null)
            {
                dialogueTriggered();
            }
            GetComponent<DialogueTrigger>().BeginDialogue();
            StartCoroutine(ResetCanInteract());
        }
    }

    public string InteractionPrompt()
    {
        if (checkEnoughAmbrosia != null)
        {
            checkEnoughAmbrosia();
        }

        if (canGift)
        {
            return "(E) Talk | (F) Gift";
        }
        else
        {
            return "(E) Talk";
        }
    }

    public void Gift() {
        if (canGift)
        {
            if (giftDialogueTriggered != null)
            {
                giftDialogueTriggered();
            }
            GetComponent<DialogueTrigger>().BeginGiftDialogue();
            canGift = false;
        }
    }

    private void SetCanGift(bool value) {
        canGift = value;
    }

    private IEnumerator ResetCanInteract() {
        yield return new WaitForSeconds(1.2f);
        canInteract = true;
    }
}
