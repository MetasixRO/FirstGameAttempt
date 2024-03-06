using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ManageDialogueBox : MonoBehaviour,IInteractable
{
    public delegate void DialogueEvent();
    public static event DialogueEvent dialogueTriggered;

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
        return "Talk";
    }
}
