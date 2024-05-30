using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class newDeadState : newBaseState
{

    private float startTime, elapsedTime;
    private float delay = 3.0f;

    private UpdatedStateManager stateManager;

    public static event BaseStateEvent RespawnPlayer;
    public static event BaseStateEvent ReachedZeroHealth;

    private static newDeadState instance;

    private newDeadState() { }

    public static newDeadState Instance
    {
        get
        {
            if (instance == null)
            {
                instance = new newDeadState();
            }
            return instance;
        }
    }

    public override void EnterState(UpdatedStateManager manager)
    {
        startTime = Time.time;
        stateManager = manager;
        ReachedZeroHealth?.Invoke();
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
        throw new System.NotImplementedException();
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
        elapsedTime = Time.time - startTime;
        if (elapsedTime >= delay && RespawnPlayer != null) {
            RespawnPlayer();
            PlayerTracker.instance.player.GetComponent<CapsuleCollider>().enabled = true;
        }
    }
}
