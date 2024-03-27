using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class newAbilityMenuState : newBaseState
{
    private bool canExit = false;

    private float startTime;
    private float delay = 0.5f;
    private float elapsedTime;

    private UpdatedStateManager stateManager;

    public static event BaseStateEvent CloseMenu;

    private static newAbilityMenuState instance;

    private newAbilityMenuState() { }

    public static newAbilityMenuState Instance
    {
        get
        {
            if (instance == null)
            {
                instance = new newAbilityMenuState();
            }
            return instance;
        }
    }

    public override void EnterState(UpdatedStateManager manager)
    {
        startTime = Time.time;
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

    public override void HandleInteract()
    {
    }

    public override void HandleMenu()
    {
        if (CloseMenu != null && canExit && stateManager.GetMenuData()) {
            CloseMenu();
        }
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
        HandleMenu();

        elapsedTime = Time.time - startTime;
        if(elapsedTime >= delay)
        {
            canExit = true;
        }
    }

    private IEnumerator DelayExit(float delay)
    {
        yield return new WaitForSeconds(delay);
    }
}
