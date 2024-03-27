using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class newBaseState
{
    public delegate void BaseStateEvent();
    public abstract void EnterState(UpdatedStateManager manager);

    public abstract void UpdateState();

    public abstract void TransitionState();

    public abstract void HandleMovement();

    public abstract void HandleInteract();

    public abstract void HandleAttack();

    public abstract void HandleMenu();

    public abstract void HandleDash();

    public abstract void ExitState();
}
