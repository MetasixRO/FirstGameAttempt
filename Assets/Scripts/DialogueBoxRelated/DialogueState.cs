using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DialogueState : BaseState
{
    public delegate void DialogueStateEvent();
    public static event DialogueStateEvent AdvanceDialogue;

    private static DialogueState instance;

    private StateManager stateMachine;


    private bool interactPressed;
    private bool canAdvance;

    private DialogueState() { }

    public static DialogueState Instance
    {
        get
        {
            if (instance == null)
            {
                instance = new DialogueState();
            }
            return instance;
        }
    }


    public override void EnterState(StateManager manager)
    {
        stateMachine = manager;

        canAdvance = true;

        stateMachine.input.CharacterControls.Use.performed += ctx => interactPressed = ctx.ReadValueAsButton();

        DialogueManager.dialogueEnded += TransitionState;
    }

    public override void TransitionState()
    {
        stateMachine.StartCoroutine(DelayTransition(0.2f));
    }

    public override void UpdateState()
    {
        HandleInteract();
    }

    private void HandleInteract() {
        if (interactPressed && AdvanceDialogue != null && canAdvance)
        {
            AdvanceDialogue();
            canAdvance = false;
            stateMachine.StartCoroutine(Delay(0.2f));
        }
    }

    IEnumerator Delay(float delay)
    {
        yield return new WaitForSeconds(delay);
        canAdvance = true;
    }

    IEnumerator DelayTransition(float delay) {
        yield return new WaitForSeconds(delay);
        stateMachine.SwitchState(LobbyState.Instance);
    }
}
