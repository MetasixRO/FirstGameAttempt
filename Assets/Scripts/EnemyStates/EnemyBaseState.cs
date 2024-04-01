using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class EnemyBaseState
{
    public abstract void EnterState(EnemyStateManager manager);

    public abstract void UpdateState();

    public abstract void ExitState();

    public abstract void HandleDash();

    public abstract void HandleMovement();

    public abstract void HandleAttack();
}
