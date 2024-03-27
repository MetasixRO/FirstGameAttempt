using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class newDialogueState : newBaseState
{
    private bool canAdvance;

    public static event BaseStateEvent DialogueAdvance;

    private UpdatedStateManager stateManager;

    private static newDialogueState instance;

    private newDialogueState() { }

    public static newDialogueState Instance
    {
        get
        {
            if (instance == null)
            {
                instance = new newDialogueState();
            }
            return instance;
        }
    }

    public override void EnterState(UpdatedStateManager manager)
    {
        stateManager = manager;
        canAdvance = false;
        DialogueManager.ContinueDialogue += ReadyInteract;
    }

    public override void HandleAttack()
    {
        throw new System.NotImplementedException();
    }

    public override void HandleDash()
    {
        throw new System.NotImplementedException();
    }

    public override void HandleInteract()
    {
        if (canAdvance && DialogueAdvance != null && stateManager.GetInteractData()) {
            DialogueAdvance();
            canAdvance = false;
        }
    }

    public override void HandleMenu()
    {
        throw new System.NotImplementedException();
    }

    public override void HandleMovement()
    {
        throw new System.NotImplementedException();
    }

    public override void TransitionState()
    {
        throw new System.NotImplementedException();
    }

    public override void UpdateState()
    {
        HandleInteract();
    }

    private void ReadyInteract() { 
        canAdvance = true;
    }

    public override void ExitState()
    {
        DialogueManager.ContinueDialogue -= ReadyInteract;
    }
}
