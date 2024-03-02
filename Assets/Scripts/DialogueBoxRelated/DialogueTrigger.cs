using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DialogueTrigger : MonoBehaviour
{
    public delegate void DialogueEvent(Dialogue dialogue);
    public static event DialogueEvent startDialogue;

    public Dialogue dialogue;

    public void BeginDialogue() { 
        if(startDialogue != null) {
            startDialogue(dialogue);    
        }
    }
}
