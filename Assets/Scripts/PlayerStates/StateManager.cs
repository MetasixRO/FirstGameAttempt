using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StateManager : MonoBehaviour
{
    private BaseState currentState;
    public Animator animator;
    public PlayerInput input;

    private void Awake()
    {
        input = new PlayerInput();
    }

    private void Start()
    {
        currentState = new LobbyState();
        animator = GetComponent<Animator>();

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
