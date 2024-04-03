using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyDeadState : EnemyBaseState
{
    private EnemyStateManager stateManager;

    private static EnemyDeadState instance;

    private EnemyDeadState() { }

    public static EnemyDeadState Instance
    {
        get
        {
            if (instance == null)
            {
                instance = new EnemyDeadState();
            }
            return instance;
        }
    }

    public override void EnterState(EnemyStateManager manager)
    {
        stateManager = manager;
    }

    public override void UpdateState()
    {
    }

    public override void ExitState()
    {
    }

    public override void HandleDash()
    {
        throw new System.NotImplementedException();
    }

    public override void HandleMovement()
    {
        throw new System.NotImplementedException();
    }

    public override void HandleAttack()
    {
        throw new System.NotImplementedException();
    }
}
