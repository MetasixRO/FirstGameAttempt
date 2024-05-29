using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyStateManager : MonoBehaviour
{
    private EnemyBaseState currentState;
    private Dictionary<string, EnemyBaseState> allStates;
    private EnemyBaseState previousState;
    private EnemyBaseState nextState;

    private AgentAnimations animations;
    private Controller controller;
    private AttackIndicator indicator;

    private bool toAttack, toArena, toFreeze, toUnfreeze, toDead, toPrevious;

    private void Start()
    {
        controller = GetComponent<Controller>();
        animations = GetComponent<AgentAnimations>();
        indicator = GetComponent<AttackIndicator>();

        InitializeStates();
        InitializeTransitions();

        currentState = allStates["Arena"];
        currentState.EnterState(this);
    }

    private void InitializeStates() {
        allStates = new Dictionary<string, EnemyBaseState>();
        allStates.Add("Arena", new EnemyArenaState());
        allStates.Add("Attack", new EnemyAttackState());
        allStates.Add("Freeze", new EnemyFreezeState());
        allStates.Add("Dead", new EnemyDeadState());
    }

    private void InitializeTransitions() {
        toAttack = false;
        toArena = false;
        toFreeze = false;
        toUnfreeze = false;
        toDead = false;
    }

    public EnemyBaseState GetCurrentState() {
        return currentState;
    }

    private void Update()
    {
        currentState.UpdateState();
    }

    public void TransitionToAttack() {
        toAttack = true;
        //indicator.Indicate();
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
        InitializeTransitions();
        toDead = true;
        StartCoroutine(DelayTransition(0.0f));
    }

    private IEnumerator DelayTransition(float delay) {
        yield return new WaitForSeconds(delay);

        if (toArena) {
            toArena = false;
            nextState = allStates["Arena"];
        }
        if (toAttack) {
            toAttack = false;
            nextState = allStates["Attack"];
        }
        if (toFreeze) {
            DenyMovement();
            toFreeze = false;
            nextState = allStates["Freeze"];
        }
        if (toUnfreeze) {
            toUnfreeze = false;
            nextState = allStates["Arena"];
        }
        if (toDead) {
            toDead = false;
            animations.enabled = false;
            controller.Freeze();
            controller.enabled = false;
            nextState = allStates["Dead"];
        }

        if (nextState != currentState) {
            //Debug.Log("Enemy Entering: " + nextState);
            indicator.Reset();
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

    public void Indicate() {
        indicator.Indicate();
    }
}
