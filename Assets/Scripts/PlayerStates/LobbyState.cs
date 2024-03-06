using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LobbyState : BaseState
{
    private static LobbyState instance;


    private StateManager stateMachine;


    private int arenaStateID = -1;
    private bool arenaNextState = false;

    private Vector2 currentMovement;
    private bool movementPressed;
    private bool runPressed;
    private bool interactPressed;


    private LobbyState() { }

    public static LobbyState Instance
    {
        get
        {
            if (instance == null)
            {
                instance = new LobbyState();
            }
            return instance;
        }
    }


    public override void EnterState(StateManager manager)
    {
        stateMachine = manager;
        WeaponPrompt.ChangeWeapon += SetArenaNextState;
        ManageDialogueBox.dialogueTriggered += SetDialogueNextState;

        stateMachine.input.CharacterControls.Movement.performed += ctx =>
        {
            currentMovement = ctx.ReadValue<Vector2>();
            movementPressed = currentMovement.magnitude > 0;
        };

        stateMachine.input.CharacterControls.Run.performed += ctx => runPressed = ctx.ReadValueAsButton();
        stateMachine.input.CharacterControls.Use.performed += ctx => interactPressed = ctx.ReadValueAsButton();
    }

    private void SetArenaNextState(int weaponID) { 
        arenaStateID = weaponID;
        arenaNextState = true;
        TransitionState();
    }

    private void SetDialogueNextState() {
        arenaNextState = false;
        TransitionState();
    }

    public override void TransitionState()
    {
        stateMachine.StartCoroutine(DelayTransition(0.2f));
    }

    IEnumerator DelayTransition(float delay)
    {
        yield return new WaitForSeconds(delay);
        if (arenaNextState)
        {
            switch (arenaStateID)
            {
                case 0: stateMachine.SwitchState(ArenaLongSword.Instance); break;
                case 1: stateMachine.SwitchState(ArenaKnife.Instance); break;
            }
        }
        else
        {
            stateMachine.movementHandler.StopAllMovement();
            stateMachine.interactHandler.StopAllInteractions();
            stateMachine.SwitchState(DialogueState.Instance);
        }

    }

    public override void UpdateState()
    {
        stateMachine.movementHandler.ReceiveMovementData(currentMovement, movementPressed, runPressed);
        stateMachine.interactHandler.ReceiveInteractButtonStatus(interactPressed);
    }

}
