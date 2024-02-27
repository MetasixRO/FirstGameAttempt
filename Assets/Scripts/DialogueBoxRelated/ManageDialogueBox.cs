using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ManageDialogueBox : MonoBehaviour,IInteractable
{
    private DialogueTrigger trigger;
    private DialogueManager manager;
    private bool firstInteract;
    public Image continueButton;

    private void Start()
    {
        trigger = GetComponent<DialogueTrigger>();
        manager = ManagerTracker.instance.dialogueManager;
        firstInteract = true;
        continueButton.enabled = false;
        StartCoroutine(ShowContinueButton(1.0f));
        DialogueManager.dialogueEnded += resetFirstInteract;
    }

    private void resetFirstInteract() {
        firstInteract = true;
    }

    public void Interact()
    {
        if (firstInteract)
        {
            trigger.TriggerDialogue();
            firstInteract = false;
        }
        else {
            manager.DisplayNextSentence();
            continueButton.enabled = false;
            StartCoroutine(ShowContinueButton(1.0f));
        }
    }

    private IEnumerator ShowContinueButton(float timer) {
        yield return new WaitForSeconds(timer);
        continueButton.enabled = true;
    }

    public string InteractionPrompt()
    {
        return "Talk";
    }
}
