using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyFreezeState : EnemyBaseState
{
    private EnemyStateManager stateManager;

    public override void EnterState(EnemyStateManager manager)
    {
        stateManager = manager;
        stateManager.OutOfFreeze(2.5f);
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
