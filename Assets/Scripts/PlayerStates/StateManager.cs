using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StateManager : MonoBehaviour
{
    private BaseState currentState;
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

        currentState.EnterState(this);
    }

    private void Update()
    {
        currentState.UpdateState();
    }

    public void SwitchState(BaseState state) {
        currentState = state;
        currentState.EnterState(this);
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
