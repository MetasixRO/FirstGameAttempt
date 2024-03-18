using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class CharacterMovement : MonoBehaviour
{

    public delegate void InitiateDash();
    public static event InitiateDash Dash;

    private bool canDash;
    private bool canRotate;

    Animator animator;
    int isWalkingHash;
    int isRunningHash;
    int isDashingHash;

    Vector2 currentMovement;
    bool movementPressed;
    bool runPressed;
    bool dashPressed;

    private void Awake()
    {
        WeaponPrompt.ChangeWeapon += ChangeStance;
        Combat.PlayerDead += ResetStance;
        ReturnToLobby.BackToLobby += ResetStance;
        NewDash.DashDone += EnableRootMotion;
        Combat.ManageWeapon += ManageRotation;
        NewDash.DelayPassed += EnableDash;
    }
    void Start()
    {
        canDash = true;
        canRotate = true;

        animator = GetComponent<Animator>();

        isWalkingHash = Animator.StringToHash("isWalking");
        isRunningHash = Animator.StringToHash("isRunning");
        isDashingHash = Animator.StringToHash("isDashing");
    }

    public void ReceiveMovementData(Vector2 receivedCurrentMovement, bool receivedMovementPressed, bool receivedRunPressed) {
        currentMovement = receivedCurrentMovement;
        movementPressed = receivedMovementPressed;
        runPressed = receivedRunPressed;
    }

    public void ReceiveDashStatus(bool receivedDashPressed) {
        dashPressed = receivedDashPressed;
    }

    void Update()
    {
            handleMovement();
            handleRotation();
            handleDash();
    }

    public void handleRotation() {
        if (canRotate)
        {
            Vector3 currentPosition = transform.position;

            Vector3 newPosition = new Vector3(currentMovement.x, 0, currentMovement.y);

            Vector3 positionToLootAt = currentPosition + newPosition;

            transform.LookAt(positionToLootAt);
        }
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

    public void handleDash() {
        if (dashPressed && Dash != null && canDash) {
            canDash = false;
            canRotate = false;
            animator.SetBool(isDashingHash, true);
            DisableRootMotion();
            Dash();
        }
    }

    private void EnableDash() {
        canDash = true;
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

    private void ResetStance() {
        animator.SetBool("isGreatSword", false);
    }

    private void DisableRootMotion() {
        animator.applyRootMotion = false;
    }

    private void EnableRootMotion() {
        canRotate = true;
        animator.SetBool(isDashingHash, false);
        animator.applyRootMotion = true;
    }

    private void ManageRotation(float damage) {
        if (canRotate) {
            canRotate = false;
        } else
        {
            canRotate = true;
        }
    }
}
