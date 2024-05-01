using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ArenaLongSword : BaseState
{
    private static ArenaLongSword instance;


    private StateManager stateMachine;

    private bool movementPressed;
    private bool interactPressed;
    private Vector2 currentMovement;
    private bool attackPressed;

    private bool dialogueNextState;


    private ArenaLongSword() { }

    public static ArenaLongSword Instance
    {
        get
        {
            if (instance == null)
            {
                instance = new ArenaLongSword();
            }
            return instance;
        }
    }

    public override void EnterState(StateManager manager)
    {
        ManageDialogueBox.dialogueTriggered += SetDialogueNextState;

        stateMachine = manager;
        stateMachine.input.CharacterControls.Movement.performed += ctx =>
        {
            currentMovement = ctx.ReadValue<Vector2>();
            movementPressed = currentMovement.magnitude > 0;
        };
        stateMachine.input.CharacterControls.Use.performed += ctx => interactPressed = ctx.ReadValueAsButton();
        stateMachine.input.CharacterControls.Shoot.performed += ctx => attackPressed = ctx.ReadValueAsButton();

        dialogueNextState = false;
    }

    public override void TransitionState()
    {
            stateMachine.SetPreviousState(this);
            stateMachine.StartCoroutine(DelayTransition(0.2f));
    }

    private IEnumerator DelayTransition(float delay) {
        yield return new WaitForSeconds(delay);
        if (dialogueNextState) {
            stateMachine.SwitchState(DialogueState.Instance);
        }
    }

    public override void UpdateState()
    {
        stateMachine.movementHandler.ReceiveMovementData(currentMovement, movementPressed, true);
        //stateMachine.interactHandler.ReceiveInteractButtonStatus(interactPressed);
       // stateMachine.attackHandler.ReceiveAttackButtonStatus(attackPressed);
    }

    private void SetDialogueNextState()
    {
        if (stateMachine.GetCurrentState() == this)
        {
            dialogueNextState=true;
            TransitionState();
        }
    }

}
