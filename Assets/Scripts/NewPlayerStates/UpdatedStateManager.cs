using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UpdatedStateManager : MonoBehaviour
{
    private newBaseState currentState;
    private newBaseState previousState;

    private PlayerInput input;

    private CharacterMovement movementHandler;
    private Combat attackHandler;
    private InteractorScript interactHandler;

    private Vector2 currentDirection;
    private bool movement, run, interact, attack, dash, menu;
    private bool toDialogue, toArena, toAttack, toMenu, toLobby, toPrevious, toDead;

    private void Awake()
    {
        input = new PlayerInput();
    }

    private void Start()
    {
        InitializeTransitions();
        SubscribeToInputs();
        ObtainComponents();
        SubscribeToEvents();
        currentState = NewLobbyState.Instance;
        previousState = null;

        currentState.EnterState(this);

        ManageDialogueBox.dialogueTriggered += TransitionToDialogue;
        DialogueManager.dialogueEnded += TransitionToPrevious;
        Combat.PlayerDead += TransitionToDead;
        ReturnToLobby.BackToLobby += TransitionToLobby;
        WeaponPrompt.ChangeWeapon += TransitionToArena;
        ManageAbilityMenu.Launched += TransitionToMenu;
        ManageAbilityMenu.Ended += TransitionToPrevious;
    }

    private void Update() {
        currentState.UpdateState();
    }

    public newBaseState GetCurrentState() { 
        return currentState;
    }

    public newBaseState GetPreviousState() {
        return previousState;
    }

    private void SubscribeToInputs() {
        input.CharacterControls.Movement.performed += ctx =>
        {
            currentDirection = ctx.ReadValue<Vector2>();
            movement = currentDirection.magnitude > 0;
        };
        input.CharacterControls.Run.performed += ctx => run = ctx.ReadValueAsButton();
        input.CharacterControls.Use.performed += ctx => interact = ctx.ReadValueAsButton();
        input.CharacterControls.Shoot.performed += ctx => attack = ctx.ReadValueAsButton();
        input.CharacterControls.Dash.performed += ctx => dash = ctx.ReadValueAsButton();
        input.CharacterControls.AbilityMenu.performed += ctx => menu = ctx.ReadValueAsButton();
    }

    private void ObtainComponents() {
        movementHandler = PlayerTracker.instance.player.GetComponent<CharacterMovement>();
        attackHandler = PlayerTracker.instance.player.GetComponent<Combat>();
        interactHandler = GetComponent<InteractorScript>();
    }

    private void SubscribeToEvents() { 
        
    }

    public void SendMovementData(bool autoRun = false)
    {
        if (autoRun)
        {
            movementHandler.ReceiveMovementData(currentDirection, movement, true);
        }
        else
        {
            movementHandler.ReceiveMovementData(currentDirection, movement, run);
        }
    }

    public void SendInteractionData() {
        interactHandler.ReceiveInteractButtonStatus(interact);
    }

    public void SendAttackData() {
        attackHandler.ReceiveAttackButtonStatus(attack);
    }

    public bool GetInteractData() {
        return interact;
    }

    public bool GetMenuData() {
        return menu;
    }

    public void SendDashData() {
        movementHandler.ReceiveDashStatus(dash);
    }

    private void OnEnable()
    {
        input.CharacterControls.Enable();
    }

    private void OnDisable()
    {
        input.CharacterControls.Disable();
    }

    private void InitializeTransitions() {
        toArena = false;
        toDialogue = false;
        toMenu = false;
        toLobby = false;
        toAttack = false;
        toPrevious = false;
    }

    private void TransitionToDialogue() {
        toDialogue = true;
        StartCoroutine(DelayTransition(0.2f));
    }

    private void TransitionToPrevious() {
        toPrevious = true;
        StartCoroutine(DelayTransition(0.2f));
    }

    private void TransitionToLobby() {
        toLobby = true;
        StartCoroutine(DelayTransition(0.2f));
    }

    private void TransitionToDead() {
        toDead = true;
        StartCoroutine(DelayTransition(0.2f));
    }

    private void TransitionToArena(int weaponID) {
        toArena = true;
        StartCoroutine(DelayTransition(0.2f));
    }

    private void TransitionToAttack() {
        toAttack = true;
        StartCoroutine(DelayTransition(0.0f));
    }

    private void TransitionOutOfAttack() {
        toArena = true;
        StartCoroutine(DelayTransition(0.0f));
    }

    private void TransitionToMenu() {
        toMenu = true;
        StartCoroutine(DelayTransition(0.2f));
    }

    private IEnumerator DelayTransition(float delay) {
        Debug.Log("Current : " + currentState);
        yield return new WaitForSeconds(delay);
        newBaseState auxiliary;
        auxiliary = currentState;
        currentState.ExitState();

        if (toDialogue)
        {
            toDialogue = false;
            currentState = newDialogueState.Instance;
            movementHandler.StopAllMovement();
        }
        if (toPrevious) {
            toPrevious = false;
            currentState = previousState;
        }
        if (toLobby) {
            toLobby = false;
            currentState = NewLobbyState.Instance;
        }
        if (toArena) {
            toArena = false;
            currentState = newArenaState.Instance;
        }
        if (toAttack) {
            toAttack = false;
            currentState = newAttackState.Instance;
        }
        if (toMenu) {
            toMenu = false;
            currentState = newAbilityMenuState.Instance;
            movementHandler.StopAllMovement();
        }

        Debug.Log("Going to : " + currentState);
        previousState = auxiliary;
        currentState.EnterState(this);
    }
}
