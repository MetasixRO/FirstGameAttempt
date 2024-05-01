using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DialogueTrigger : MonoBehaviour
{
    public delegate void DialogueEvent(Dialogue dialogue);
    public static event DialogueEvent startDialogue;

    public delegate void DialogueErrorEvent();
    public static event DialogueErrorEvent NoDialogueLeft;

    public delegate void SendDialogueStatus(int npcID, string npcName, int currentRank);
    public static event SendDialogueStatus DialogueStatus;

    private bool alreadyStarted = false;
    private float delayTimer = 0.5f;

    public Dialogue[] dialogues;
    public Dialogue[] giftDialogues;

    private static int nextID = 1;
    private int npcID;
    int counter = 0;
    int giftCounter = 0;

    private void Awake()
    {
        npcID = nextID;
        nextID++;
    }

    private void Start()
    {
        if (DialogueStatus != null)
        {
            Debug.Log("DialogueTrigger send the event");
            DialogueStatus(npcID, dialogues[0].characterName, giftCounter);
        }
    }

    public void BeginDialogue() {
        if (startDialogue != null && !alreadyStarted && counter < dialogues.Length)
        {
            startDialogue(dialogues[counter]);
            alreadyStarted = true;
            StartCoroutine(DelayAnotherDialogue(delayTimer));
            counter++;
            if (counter == dialogues.Length) {
                counter = dialogues.Length - 1;
            }
            

        }
        else { 
            if(NoDialogueLeft != null) {
                NoDialogueLeft();
            }
        }
    }

    public void BeginGiftDialogue() {
        if (startDialogue != null && !alreadyStarted && giftCounter < giftDialogues.Length)
        {
            startDialogue(giftDialogues[giftCounter]);
            alreadyStarted = true;
            StartCoroutine(DelayAnotherDialogue(delayTimer));
            giftCounter++;

            if (DialogueStatus != null)
            {
                DialogueStatus(npcID, dialogues[0].characterName, giftCounter);
            }

            if (giftCounter == 5) {
                GetComponent<PersonalAbilityHolder>().AddAbilityToTheBoon();
            }
        }
        else
        {
            if (NoDialogueLeft != null)
            {
                NoDialogueLeft();
            }
        }
    }

    private IEnumerator DelayAnotherDialogue(float delay) {
        yield return new WaitForSeconds(delay);
        alreadyStarted = false;
    }
}
