using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NewLobbyState : newBaseState
{
    private UpdatedStateManager stateManager;

    public static event BaseStateEvent LobbyInteract;

    private static NewLobbyState instance;

    private NewLobbyState() { }

    public static NewLobbyState Instance
    {
        get
        {
            if (instance == null)
            {
                instance = new NewLobbyState();
            }
            return instance;
        }
    }

    public override void EnterState(UpdatedStateManager manager)
    {
        stateManager = manager;
    }

    public override void HandleAttack()
    {
    }

    public override void HandleMenu()
    {
    }

    public override void HandleMovement()
    {
        stateManager.SendMovementData();
    }

    public override void TransitionState()
    {
    }

    public override void UpdateState()
    {
        HandleMovement();
        HandleInteract();
    }

    public override void HandleDash()
    {
    }

    public override void HandleInteract()
    {
        stateManager.SendInteractionData();
    }

    public override void ExitState() { 
        
    }

}
