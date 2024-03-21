using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StateManager : MonoBehaviour
{
    private BaseState currentState;
    private BaseState previousState;

    public PlayerInput input;
    public GameObject player;

    public InteractorScript interactHandler;
    public CharacterMovement movementHandler;
    public Combat attackHandler;


    private void Awake()
    {
        input = new PlayerInput();
    }

    private void Start()
    {
        player = PlayerTracker.instance.player;

        interactHandler = player.GetComponent<InteractorScript>();
        movementHandler = player.GetComponent<CharacterMovement>();
        attackHandler = player.GetComponent<Combat>();


        currentState = LobbyState.Instance;
        previousState = null;

        currentState.EnterState(this);
    }

    private void Update()
    {
        //Debug.Log(currentState);
        currentState.UpdateState();
    }

    public void SwitchState(BaseState state) {
        Debug.Log("Swtiching to : " + state);
        currentState = state;
        currentState.EnterState(this);
    }

    public BaseState GetCurrentState() {
        return currentState;
    }

    public BaseState GetPreviousState() {
        return previousState;
    }

    public void SetPreviousState(BaseState state) { 
        if(state != null)
        {
            previousState = state;
        }
    }

    void OnEnable()
    {
        input.CharacterControls.Enable();
    }

    void OnDisable()
    {
        input.CharacterControls.Disable();
    }
}
