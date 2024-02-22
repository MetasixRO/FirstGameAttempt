using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class BattleScript : MonoBehaviour
{
    public bool canAttack = true;
    public float attackCooldown = 1.0f;

    Animator animator;
    int isAttackingHash;

    PlayerInput input;

    bool attackPressed;


    // Start is called before the first frame update
    void Start()
    {
        animator = GetComponent<Animator>();
        isAttackingHash = Animator.StringToHash("isAttacking");
    }

    // Update is called once per frame
    void Update()
    {
        bool isAttacking = animator.GetBool(isAttackingHash);
        
        //test
        input.CharacterControls.Shoot.performed += ctx => attackPressed = ctx.ReadValueAsButton();
        if (attackPressed && canAttack)
        {
            handleAttack();
        }

        if (!attackPressed && isAttacking)
        {
            animator.SetBool(isAttackingHash, false);
        }
    }

    void handleAttack()
    {
        animator.SetBool(isAttackingHash, true);
        canAttack = false;
        StartCoroutine(resetAttackCooldown());
    }

    IEnumerator resetAttackCooldown()
    {
        yield return new WaitForSeconds(attackCooldown);
        canAttack = true;
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
