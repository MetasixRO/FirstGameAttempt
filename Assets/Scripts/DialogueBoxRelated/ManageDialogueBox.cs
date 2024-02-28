using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ManageDialogueBox : MonoBehaviour,IInteractable
{
    public delegate void DialogueEvent();
    public static event DialogueEvent dialogueTriggered;

    private DialogueManager manager;
    private bool firstInteract;
    public Dialogue dialogue;
    public Image continueButton;

    private void Start()
    {
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
            if (dialogueTriggered != null) {
                dialogueTriggered();
            }
            manager.StartDialogue(dialogue);
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
