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

    private void Start()
    {
        canGift = false;
        ResourceManager.CheckAmbrosiaResult += SetCanGift;
    }

    public void Interact()
    {
        if (dialogueTriggered != null)
        {
            dialogueTriggered();
        }
        GetComponent<DialogueTrigger>().BeginDialogue();
    }

    public string InteractionPrompt()
    {
        if (checkEnoughAmbrosia != null)
        {
            checkEnoughAmbrosia();
        }

        if (gameObject.CompareTag("EasterEgg"))
        {
            canGift = false;
            return ("(E) Crowbar");
            
        }
        else
        {

            if (canGift)
            {
                return "(E) Talk | (F) Gift";
            }
            else
            {
                return "(E) Talk";
            }
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
}
