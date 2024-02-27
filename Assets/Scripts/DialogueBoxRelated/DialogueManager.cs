using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class DialogueManager : MonoBehaviour
{

    public delegate void DialogueCallback();
    public static event DialogueCallback dialogueEnded;

    private Queue<string> sentences;

    public TextMeshProUGUI characterName;
    public TextMeshProUGUI dialogueText;

    public Animator animator;

    private void Start()
    {
        sentences = new Queue<string>();
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

        string sentence = sentences.Dequeue();
        dialogueText.text = sentence;
    }

    private void EndDialogue() {
        animator.SetBool("isOpen", false);
        if (dialogueEnded != null) {
            dialogueEnded();
        }
    }
}
