using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class DialogueManager : MonoBehaviour
{

    public delegate void DialogueCallback();
    public static event DialogueCallback dialogueEnded;
    public static event DialogueCallback ContinueDialogue;

    private Queue<string> sentences;

    public TextMeshProUGUI characterName;
    public TextMeshProUGUI dialogueText;
    public Image continueButton;

    public Animator animator;

    private void Start()
    {
        sentences = new Queue<string>();
        continueButton.enabled = false;
        StartCoroutine(ShowContinueButton(1.0f));
        DialogueTrigger.startDialogue += StartDialogue;
        DialogueState.AdvanceDialogue += DisplayNextSentence;
        newDialogueState.DialogueAdvance += DisplayNextSentence;
    }

    public void StartDialogue(Dialogue dialogue) {

        animator.SetBool("isOpen", true);

        characterName.text = dialogue.characterName;

        sentences.Clear();

        foreach (string sentence in dialogue.sentences) { 
            sentences.Enqueue(sentence);
        }

        DisplayNextSentence();
    }

    public void DisplayNextSentence() {
        if (sentences.Count == 0) {
            EndDialogue();
            return;
        }
        continueButton.enabled = false;

        string sentence = sentences.Dequeue();
        dialogueText.text = sentence;
        StartCoroutine(ShowContinueButton(1.0f));
    }

    private void EndDialogue() {
        animator.SetBool("isOpen", false);
        if (dialogueEnded != null) {
            dialogueEnded();
        }
    }

    private IEnumerator ShowContinueButton(float timer)
    {
        yield return new WaitForSeconds(timer);
        continueButton.enabled = true;
        if (ContinueDialogue != null) {
            ContinueDialogue();
        }
    }
}
