using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AbilityMenuState : BaseState
{

    public delegate void ToggleAbilityMenu();
    public static event ToggleAbilityMenu OpenMenu;
    public static event ToggleAbilityMenu CloseMenu;

    private static AbilityMenuState instance;

    private StateManager stateMachine;

    private bool isMenuPressed;
    private bool isOpen = false;
    private bool allowToLeave = false;

    private AbilityMenuState() { }

    public static AbilityMenuState Instance 
    {
        get 
        {
            if (instance == null) {
                instance = new AbilityMenuState();
            }
            return instance;
        }
    }

    public override void EnterState(StateManager manager)
    {
        stateMachine = manager;

        if (OpenMenu != null && !isOpen) {
            isOpen = true;
            OpenMenu();
        }
        stateMachine.movementHandler.StopAllMovement();
        stateMachine.interactHandler.StopAllInteractions();
        stateMachine.StartCoroutine(DelayLeaving(1.0f));
        stateMachine.input.CharacterControls.AbilityMenu.performed += ctx => isMenuPressed = ctx.ReadValueAsButton();
    }

    public override void TransitionState()
    {
        if (stateMachine.GetCurrentState() == this)
        {
            stateMachine.StartCoroutine(DelayTransition(0.2f));
        }
    }

    public override void UpdateState()
    {
        if (isMenuPressed && CloseMenu != null && isOpen && allowToLeave) {
            CloseMenu();
            isOpen = false;
            TransitionState();
        }
    }

    private IEnumerator DelayTransition(float delay)
    {
        yield return new WaitForSeconds(delay);
        stateMachine.SwitchState(ArenaState.Instance);
    }

    private IEnumerator DelayLeaving(float delay) {
        yield return new WaitForSeconds(delay);
        allowToLeave = true;
    }
}
