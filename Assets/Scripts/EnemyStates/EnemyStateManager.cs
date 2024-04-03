using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyStateManager : MonoBehaviour
{
    private EnemyBaseState currentState;
    private EnemyBaseState previousState;
    private EnemyBaseState nextState;

    private AgentController controller;
    private AgentAnimations animations;

    private bool toAttack, toArena, toFreeze, toUnfreeze, toDead, toPrevious;

    private void Start()
    {
        controller = GetComponent<AgentController>();
        animations = GetComponent<AgentAnimations>();

        InitializeTransitions();

        currentState = EnemyArenaState.Instance;
        currentState.EnterState(this);
        //Debug.Log("Enemy is Now : " + currentState);
    }

    private void InitializeTransitions() {
        toAttack = false;
        toArena = false;
        toFreeze = false;
        toUnfreeze = false;
        toDead = false;
    //    toPrevious = false;
    }

    private void Update()
    {
        currentState.UpdateState();
    }

    public void TransitionToAttack() {
        toAttack = true;
        StartCoroutine(DelayTransition(0.0f));
    }

    public void TransitionToArena() {
        toArena = true;
        StartCoroutine(DelayTransition(0.0f));
    }

    public void OutOfFreeze(float delay) {
        toUnfreeze = true;
        StartCoroutine(DelayTransition(delay));
    }

    public void TransitionToFreeze() {
        toFreeze = true;
        StartCoroutine(DelayTransition(0.0f));
    }

    public void TransitionToDead() {
        toDead = true;
        StartCoroutine(DelayTransition(0.0f));
    }

    private IEnumerator DelayTransition(float delay) {
        yield return new WaitForSeconds(delay);

        if (toArena) {
            toArena = false;
            nextState = EnemyArenaState.Instance;
        }
        if (toAttack) {
            toAttack = false;
            nextState = EnemyAttackState.Instance;
        }
        if (toFreeze) {
            DenyMovement();
            toFreeze = false;
            nextState = EnemyFreezeState.Instance;
        }
        if (toUnfreeze) {
            toUnfreeze = false;
            nextState = EnemyArenaState.Instance;
        }
        if (toDead) {
            toDead = false;
            nextState = EnemyDeadState.Instance;
        }

        if (nextState != currentState) {
            //Debug.Log("Enemy Entering: " + nextState);
            previousState = currentState;
            currentState = nextState;
            currentState.EnterState(this);
        }
    }

    public Vector3 GetEnemyCurrentPosition() {
        return transform.position;
    }

    public void DenyMovement() {
        controller.Freeze();
        animations.Freeze();
    }

    public void AllowMovement() {
        controller.Movement();
        animations.HandleMovement();
    }

    public void AllowRotation() {
        animations.Rotate();
    }

    public void AllowCombat() {
        animations.HandleCombat();
    }

    public bool CombatChecks() {
        return animations.CheckForCombat();
    }
}
