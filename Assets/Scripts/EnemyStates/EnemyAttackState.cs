using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAttackState : EnemyBaseState
{

    private EnemyStateManager stateManager;

    private bool alreadyAttacking;

    public override void EnterState(EnemyStateManager manager)
    {
        stateManager = manager;
        alreadyAttacking = false;
    }

    public override void ExitState()
    {
        stateManager.ResetIndicate();
    }

    public override void HandleAttack()
    {
        stateManager.AllowRotation();
        if (!alreadyAttacking)
        {
            stateManager.StartCoroutine(DelayAttack());
            alreadyAttacking = true;
        }
        if (!stateManager.CombatChecks()) {
            stateManager.TransitionToArena();
        }
    }

    private IEnumerator DelayAttack() {
        stateManager.Indicate();
        yield return new WaitForSeconds(1.0f);
        if (stateManager.GetCurrentState() is EnemyAttackState) {
           // Debug.Log("Attacking");
            stateManager.AllowCombat();
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
