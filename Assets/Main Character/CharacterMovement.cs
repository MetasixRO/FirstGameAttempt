using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class CharacterMovement : MonoBehaviour
{
    Animator animator;
    int isWalkingHash;
    int isRunningHash;
   

    PlayerInput input;

    Vector2 currentMovement;
    bool movementPressed;
    bool runPressed;

    private void Awake()
    {
        input = new PlayerInput();

        //ctx :  current context
        input.CharacterControls.Movement.performed += ctx =>
        {
            currentMovement = ctx.ReadValue<Vector2>();
            movementPressed = currentMovement.magnitude > 0;
        };
        input.CharacterControls.Run.performed += ctx => runPressed = ctx.ReadValueAsButton();
    }
    void Start()
    {
        animator = GetComponent<Animator>();

        isWalkingHash = Animator.StringToHash("isWalking");
        isRunningHash = Animator.StringToHash("isRunning");
    }

    void Update()
    {
        handleMovement();
        handleRotation();
    }

    void handleRotation() { 
        Vector3 currentPosition = transform.position;

        Vector3 newPosition = new Vector3(currentMovement.x, 0, currentMovement.y);

        Vector3 positionToLootAt = currentPosition + newPosition;

        transform.LookAt(positionToLootAt);
    }

    void handleMovement()
    {
        bool isRunning = animator.GetBool(isRunningHash);
        bool isWalking = animator.GetBool(isWalkingHash);


        if (movementPressed && !isWalking) { 
            animator.SetBool(isWalkingHash, true);
        }

        if (!movementPressed && isWalking) {
            animator.SetBool(isWalkingHash, false);   
        }

        if (movementPressed && runPressed && !isRunning) {
            animator.SetBool(isRunningHash, true);
        }

        if ((!movementPressed || !runPressed) && isRunning) {
            animator.SetBool(isRunningHash, false);
        }

        if (!runPressed && isRunning) {
            animator.SetBool(isRunningHash, false);
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
