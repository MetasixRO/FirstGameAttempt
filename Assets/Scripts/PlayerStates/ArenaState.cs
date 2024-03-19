using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ArenaState : BaseState
{
    private static ArenaState instance;

    private StateManager stateMachine;

    private bool movementPressed, interactPressed, attackPressed, dashPressed, menuPressed;
    private Vector2 currentMovement;

    private bool dialogueNextState, deadNextState, lobbyNextState, abilityMenuNextState;

    private ArenaState() { }

    public static ArenaState Instance
    {
        get
        {
            if (instance == null)
            {
                instance = new ArenaState();
            }
            return instance;
        }
    }

    public override void EnterState(StateManager manager)
    {
        ManageDialogueBox.dialogueTriggered += SetDialogueNextState;
        Combat.PlayerDead += SetDeadNextState;
        ReturnToLobby.BackToLobby += SetLobbyNextState;

        stateMachine = manager;

        SubscribeToInputs();
        InitializeNextState();
    }

    public override void TransitionState()
    {
        stateMachine.SetPreviousState(this);
        stateMachine.StartCoroutine(DelayTransition(0.2f));
    }

    private IEnumerator DelayTransition(float delay)
    {
        yield return new WaitForSeconds(delay);
        if (dialogueNextState)
        {
            stateMachine.SwitchState(DialogueState.Instance);
        }
        else if (deadNextState)
        {
            stateMachine.SwitchState(DeadState.Instance);
        }
        else if (lobbyNextState)
        {
            stateMachine.SwitchState(LobbyState.Instance);
        }
        else if (abilityMenuNextState) {
            stateMachine.SwitchState(AbilityMenuState.Instance);
        }

    }

    public override void UpdateState()
    {
        stateMachine.movementHandler.ReceiveMovementData(currentMovement, movementPressed, true);
        stateMachine.interactHandler.ReceiveInteractButtonStatus(interactPressed);
        stateMachine.attackHandler.ReceiveAttackButtonStatus(attackPressed);
        stateMachine.movementHandler.ReceiveDashStatus(dashPressed);
        if (menuPressed && stateMachine.GetCurrentState() == this) {
            SetAbilityMenuNextState();
        }
    }

    private void SetAbilityMenuNextState() { 
        if(stateMachine.GetCurrentState() == this)
        {
            abilityMenuNextState = true;
            TransitionState();
        }
    }

    private void SetDialogueNextState()
    {
        if (stateMachine.GetCurrentState() == this)
        {
            dialogueNextState = true;
            TransitionState();
        }
    }

    private void SetDeadNextState() { 
        if(stateMachine.GetCurrentState() == this)
        {
            deadNextState = true;
            TransitionState();
        }
    }

    private void SetLobbyNextState() { 
        if(stateMachine.GetCurrentState() == this)
        {
            lobbyNextState = true;
            TransitionState();
        }
    }

    private void InitializeNextState() {
        dialogueNextState = false;
        deadNextState = false;
        lobbyNextState = false;
        abilityMenuNextState = false;
    }

    private void SubscribeToInputs() {
        stateMachine.input.CharacterControls.Movement.performed += ctx =>
        {
            currentMovement = ctx.ReadValue<Vector2>();
            movementPressed = currentMovement.magnitude > 0;
        };
        stateMachine.input.CharacterControls.Use.performed += ctx => interactPressed = ctx.ReadValueAsButton();
        stateMachine.input.CharacterControls.Shoot.performed += ctx => attackPressed = ctx.ReadValueAsButton();
        stateMachine.input.CharacterControls.Dash.performed += ctx => dashPressed = ctx.ReadValueAsButton();
        stateMachine.input.CharacterControls.AbilityMenu.performed += ctx => menuPressed = ctx.ReadValueAsButton();
    }
}
