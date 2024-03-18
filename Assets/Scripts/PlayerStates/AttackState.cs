using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttackState : BaseState
{
    private StateManager stateMachine;

    public static AttackState instance;

    private AttackState() { }

    public static AttackState Instance
    {
        get 
        {
            if (instance == null) {
                instance = new AttackState();
            }
            return instance;
        }
    }



    public override void EnterState(StateManager manager)
    {
        stateMachine = manager;

    }

    public override void TransitionState()
    {
        stateMachine.SetPreviousState(this);
        stateMachine.StartCoroutine(DelayTransition(0.2f));
    }

    private IEnumerator DelayTransition(float delay) {
        yield return new WaitForSeconds(delay);
        stateMachine.SwitchState(ArenaState.Instance);
    }

    public override void UpdateState()
    {
    }

    private void SetArenaNextState() {
        if (stateMachine.GetCurrentState() == this) {
            TransitionState();
        }
    }
}
