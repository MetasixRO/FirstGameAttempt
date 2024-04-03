using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemyArenaState : EnemyBaseState
{

    //[SerializeField] private float stoppingDistance = 1.5f;
    private NavMeshAgent agent;
    private bool destinationIsCurrentPosition;

    private EnemyStateManager stateManager;

    private static EnemyArenaState instance;

    private EnemyArenaState() { }

    public static EnemyArenaState Instance {
        get
        {
            if (instance == null) { 
                instance = new EnemyArenaState();
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
        throw new System.NotImplementedException();
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
        HandleMovement();
        HandleAttack();
    }
}
