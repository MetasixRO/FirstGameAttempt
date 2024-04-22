using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemyArenaState : EnemyBaseState
{
    private EnemyStateManager stateManager;

    public override void EnterState(EnemyStateManager manager)
    {
        stateManager = manager;
    }

    public override void ExitState()
    {
    }

    public override void HandleAttack()
    {
        bool result = stateManager.CombatChecks();
        if (result)
        {
            stateManager.TransitionToAttack();
        }
    }

    public override void HandleDash()
    {
        throw new System.NotImplementedException();
    }

    public override void HandleMovement()
    {
        stateManager.AllowMovement();
        stateManager.AllowRotation();
    }

    public override void UpdateState()
    {
        HandleMovement();
        HandleAttack();
    }
}
