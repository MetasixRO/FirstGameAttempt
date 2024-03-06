using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ArenaKnife : BaseState
{

    private static ArenaKnife instance;

    private StateManager stateMachine;

    private bool movementPressed;
    private bool interactPressed;
    private Vector2 currentMovement;
    private bool attackPressed;

    private ArenaKnife() { }

    public static ArenaKnife Instance
    {
        get
        {
            if (instance == null)
            {
                instance = new ArenaKnife();
            }
            return instance;
        }
    }

    public override void EnterState(StateManager manager)
    {
        stateMachine = manager;
        stateMachine.input.CharacterControls.Movement.performed += ctx =>
        {
            currentMovement = ctx.ReadValue<Vector2>();
            movementPressed = currentMovement.magnitude > 0;
        };
        stateMachine.input.CharacterControls.Use.performed += ctx => interactPressed = ctx.ReadValueAsButton();
        stateMachine.input.CharacterControls.Shoot.performed += ctx => attackPressed = ctx.ReadValueAsButton();
    }

    public override void TransitionState()
    {
        throw new System.NotImplementedException();
    }

    public override void UpdateState()
    {
        stateMachine.movementHandler.ReceiveMovementData(currentMovement, movementPressed, true);
        stateMachine.interactHandler.ReceiveInteractButtonStatus(interactPressed);
    }
}
