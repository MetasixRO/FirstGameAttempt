using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class CharacterMovement : MonoBehaviour
{
    Animator animator;
    int isWalkingHash;
    int isRunningHash;
    private bool freeze;
   

    PlayerInput input;

    Vector2 currentMovement;
    bool movementPressed;
    bool runPressed;

    private void Awake()
    {
        freeze = false;
        ManageDialogueBox.dialogueTriggered += ManageFreeze;
        DialogueManager.dialogueEnded += ManageFreeze;
        WeaponPrompt.ChangeWeapon += ChangeStance;

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
        if (!freeze)
        {
            handleMovement();
            handleRotation();
        }
    }

    public void handleRotation() { 
        Vector3 currentPosition = transform.position;

        Vector3 newPosition = new Vector3(currentMovement.x, 0, currentMovement.y);

        Vector3 positionToLootAt = currentPosition + newPosition;

        transform.LookAt(positionToLootAt);
    }

    public void handleMovement()
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

    private void ManageFreeze() {
        if (freeze)
        {
            freeze = false;
        }
        else { 
            freeze = true;
            animator.SetBool(isWalkingHash, false);
            animator.SetBool(isRunningHash, false);
        }
    }

    private void ChangeStance(int weaponID)
    {
        switch (weaponID)
        {
            case 0:
                animator.SetBool("isGreatSword", true);
                break;
            case 1:
                animator.SetBool("isGreatSword", false);
                break;
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
