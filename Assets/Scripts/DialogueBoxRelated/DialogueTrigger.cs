using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DialogueTrigger : MonoBehaviour
{
    public delegate void DialogueEvent(Dialogue dialogue);
    public static event DialogueEvent startDialogue;

    public delegate void DialogueErrorEvent();
    public static event DialogueErrorEvent NoDialogueLeft;
    public static event DialogueErrorEvent NoGiftDialogueLeft;


    public delegate void SendDialogueStatus(int npcID, string npcName, int currentRank);
    public static event SendDialogueStatus DialogueStatus;

    private bool alreadyStarted = false;
    private float delayTimer = 0.5f;

    public Dialogue[] dialogues;
    public Dialogue[] giftDialogues;

    private static int nextID = 1;
    private int npcID;
    private int counter = 0;
    private int giftCounter = 0;
    private bool shouldAdvance;
    private bool shouldCheck;

    private void Awake()
    {
        npcID = nextID;
        nextID++;
    }

    private void Start()
    {
        shouldCheck = true;
        shouldAdvance = true;
        Combat.PlayerDead += AdvanceDialogue;
        if (DialogueStatus != null)
        {
            DialogueStatus(npcID, dialogues[0].characterName, giftCounter);
        }
    }

    private void Update()
    {
        if (Input.GetKeyDown("o")) {
            giftCounter = 4;
            Debug.Log("level 4 Friendship");
        }
    }

    public void BeginDialogue() {
        if (shouldCheck)
        {
            shouldCheck = false;
            StartCoroutine(DelayCheckingDialogueBeginning(delayTimer));
            if (startDialogue != null && !alreadyStarted && counter < dialogues.Length && shouldAdvance)
            {
                startDialogue?.Invoke(dialogues[counter]);
                alreadyStarted = true;
                shouldAdvance = false;
                StartCoroutine(DelayAnotherDialogue(delayTimer));
            }
            else
            {
                if (NoDialogueLeft != null)
                {
                    NoDialogueLeft();
                }
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
            NoGiftDialogueLeft?.Invoke();
        }
    }

    private IEnumerator DelayAnotherDialogue(float delay) {
        yield return new WaitForSeconds(delay);
        alreadyStarted = false;
    }

    private IEnumerator DelayCheckingDialogueBeginning(float delay) {
        yield return new WaitForSeconds(2.0f);
        shouldCheck = true;
    }

    private void AdvanceDialogue() {
        if (!shouldAdvance)
        {
            Debug.Log("Advancing here:");
            counter++;
            shouldAdvance = true;
            if (counter == dialogues.Length)
            {
                counter = dialogues.Length - 1;
            }
        }
    }
}
