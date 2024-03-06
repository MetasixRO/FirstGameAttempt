using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LobbyState : BaseState
{
    private StateManager stateMachine;

    private int arenaStateID = -1;
    private bool arenaNextState = false;

    private Vector2 currentMovement;
    private bool movementPressed;
    private bool runPressed;
    private bool interactPressed;

    private int isWalkingHash;
    private int isRunningHash;



    public override void EnterState(StateManager manager)
    {
        stateMachine = manager;
        WeaponPrompt.ChangeWeapon += SetArenaNextState;
        DialogueTrigger.startDialogue += SetDialogueNextState;

        stateMachine.input.CharacterControls.Movement.performed += ctx =>
        {
            currentMovement = ctx.ReadValue<Vector2>();
            movementPressed = currentMovement.magnitude > 0;
        };

        stateMachine.input.CharacterControls.Run.performed += ctx => runPressed = ctx.ReadValueAsButton();
        stateMachine.input.CharacterControls.Use.performed += ctx => interactPressed = ctx.ReadValueAsButton();

        isWalkingHash = Animator.StringToHash("isWalking");
        isRunningHash = Animator.StringToHash("isRunning");
    }

    private void SetArenaNextState(int weaponID) { 
        arenaStateID = weaponID;
        arenaNextState = true;
        TransitionState();
    }

    private void SetDialogueNextState(Dialogue dialogue) {
        arenaNextState = false;
        TransitionState();
    }

    public override void TransitionState()
    {
        if (arenaNextState)
        {
            switch (arenaStateID)
            {
                case 0: stateMachine.SwitchState(new ArenaLongSword()); break;
                case 1: stateMachine.SwitchState(new ArenaKnife()); break;
            }
        }
        else 
        {
            stateMachine.SwitchState(new DialogueState());
        }
    }

    public override void UpdateState()
    {
        HandleRotation();
        HandleMovement();
        HandleInteract();
    }

    private void HandleRotation() {
        Vector3 currentPosition = PlayerTracker.instance.player.transform.position;

        Vector3 newPosition = new Vector3(currentMovement.x, 0, currentMovement.y);

        Vector3 positionToLootAt = currentPosition + newPosition;

        PlayerTracker.instance.player.transform.LookAt(positionToLootAt);
    }

    private void HandleMovement() {
        bool isRunning = stateMachine.animator.GetBool(isRunningHash);
        bool isWalking = stateMachine.animator.GetBool(isWalkingHash);


        if (movementPressed && !isWalking)
        {
            stateMachine.animator.SetBool(isWalkingHash, true);
        }

        if (!movementPressed && isWalking)
        {
            stateMachine.animator.SetBool(isWalkingHash, false);
        }

        if (movementPressed && runPressed && !isRunning)
        {
            stateMachine.animator.SetBool(isRunningHash, true);
        }

        if ((!movementPressed || !runPressed) && isRunning)
        {
            stateMachine.animator.SetBool(isRunningHash, false);
        }

        if (!runPressed && isRunning)
        {
            stateMachine.animator.SetBool(isRunningHash, false);
        }
    }

    private void HandleInteract() { 
        
    }

}
