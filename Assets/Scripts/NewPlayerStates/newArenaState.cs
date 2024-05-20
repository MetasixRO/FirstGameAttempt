using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class newArenaState : newBaseState
{
    private UpdatedStateManager stateManager;

    public static event BaseStateEvent OpenMenu;

    private static newArenaState instance;

    private newArenaState() { }

    public static newArenaState Instance
    {
        get
        {
            if (instance == null)
            {
                instance = new newArenaState();
            }
            return instance;
        }
    }

    public override void EnterState(UpdatedStateManager manager)
    {
        stateManager = manager;
    }

    public override void ExitState()
    {
    }

    public override void HandleAttack()
    {
        stateManager.SendAttackData();
        stateManager.SendNPCSpecialData();
    }

    public override void HandleDash()
    {
        stateManager.SendDashData();
    }

    public override void HandleInteract()
    {
        stateManager.SendInteractionData();

    }

    public override void HandleMenu()
    {
        if (OpenMenu != null && stateManager.GetMenuData()) {
            OpenMenu();
        }
    }

    public override void HandleMovement()
    {
        stateManager.SendMovementData(true);
    }

    public override void TransitionState()
    {
        throw new System.NotImplementedException();
    }

    public override void UpdateState()
    {
        HandleAttack();
        HandleMovement();
        HandleInteract();
        HandleMenu();
        HandleDash();
    }
}
