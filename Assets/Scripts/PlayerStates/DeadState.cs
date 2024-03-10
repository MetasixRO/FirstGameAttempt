using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeadState : BaseState
{
    public delegate void RespawnEvent();
    public static event RespawnEvent RespawnPlayer;

    private StateManager stateMachine;

    private static DeadState instance;

    private DeadState() { }

    public static DeadState Instance
    {
        get 
        {
            if (instance == null)
            {
                instance = new DeadState();
            }
            return instance;
        }
    }

    public override void EnterState(StateManager manager)
    {
        stateMachine = manager;
        stateMachine.StartCoroutine(Respawn(3.0f));
    }

    public override void TransitionState()
    {
        stateMachine.SetPreviousState(this);
        stateMachine.StartCoroutine(Delay(0.2f));
    }

    public override void UpdateState()
    {
    }

    private IEnumerator Respawn(float delay) {
        yield return new WaitForSeconds(delay);
        if (RespawnPlayer != null) {
            RespawnPlayer();
            stateMachine.attackHandler.RestoreHealth(999);
            TransitionState();
        }
    }

    private IEnumerator Delay(float delay)
    {
        yield return new WaitForSeconds(delay);
        stateMachine.SwitchState(LobbyState.Instance);
    }
}
