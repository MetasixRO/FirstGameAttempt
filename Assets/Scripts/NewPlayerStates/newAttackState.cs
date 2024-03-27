using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class newAttackState : newBaseState
{
    private UpdatedStateManager stateManager;

    private static newAttackState instance;

    private newAttackState() { }

    public static newAttackState Instance
    {
        get
        {
            if (instance == null)
            {
                instance = new newAttackState();
            }
            return instance;
        }
    }

    public override void EnterState(UpdatedStateManager manager)
    {
        stateManager = manager;
    }

    public override void ExitState()
    {
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
        throw new System.NotImplementedException();
    }

    public override void HandleMenu()
    {
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
        HandleMenu();
    }

    
}
