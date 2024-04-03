using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAttackState : EnemyBaseState
{

    private EnemyStateManager stateManager;

    private static EnemyAttackState instance;

    private EnemyAttackState() { }

    public static EnemyAttackState Instance
    {
        get
        {
            if (instance == null)
            {
                instance = new EnemyAttackState();
            }
            return instance;
        }
    }

    public override void EnterState(EnemyStateManager manager)
    {
        stateManager = manager;
    }

    public override void ExitState()
    {
    }

    public override void HandleAttack()
    {
        stateManager.AllowRotation();
        stateManager.AllowCombat();
        if (!stateManager.CombatChecks()) {
            stateManager.TransitionToArena();
        }
    }

    public override void HandleDash()
    {
        throw new System.NotImplementedException();
    }

    public override void HandleMovement()
    {
        throw new System.NotImplementedException();
    }

    public override void UpdateState()
    {
        HandleAttack();
    }
}
