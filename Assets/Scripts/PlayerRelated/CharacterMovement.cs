using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class CharacterMovement : MonoBehaviour
{
    Animator animator;
    int isWalkingHash;
    int isRunningHash;

    Vector2 currentMovement;
    bool movementPressed;
    bool runPressed;

    private void Awake()
    {
        WeaponPrompt.ChangeWeapon += ChangeStance;
    }
    void Start()
    {
        animator = GetComponent<Animator>();

        isWalkingHash = Animator.StringToHash("isWalking");
        isRunningHash = Animator.StringToHash("isRunning");
    }

    public void ReceiveMovementData(Vector2 receivedCurrentMovement, bool receivedMovementPressed, bool receivedRunPressed) {
        currentMovement = receivedCurrentMovement;
        movementPressed = receivedMovementPressed;
        runPressed = receivedRunPressed;
    }

    void Update()
    {
            handleMovement();
            handleRotation();
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

    public void StopAllMovement() {
        movementPressed = false;
        runPressed = false;
        animator.SetBool(isWalkingHash, false);
        animator.SetBool(isRunningHash, false);
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
}
