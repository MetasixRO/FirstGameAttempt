using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class AgentAnimations : MonoBehaviour
{
    private NavMeshAgent agent;
    private Animator animator;

    private int isRunningHash;
    private int isAttackingHash;

    private bool canAttack;

    private Transform player;

    void Start()
    {
        agent = GetComponent<NavMeshAgent>();
        animator = GetComponent<Animator>();
        isRunningHash = Animator.StringToHash("Running");
        isAttackingHash = Animator.StringToHash("Punch");
        player = PlayerTracker.instance.player.transform;
        canAttack = true;
    }

    void Update()
    {
        HandleMovement();
        HandleCombat();
    }

    private void HandleMovement() {
        if (agent.velocity.magnitude > 0.1f && !animator.GetBool(isRunningHash)) {
            animator.SetBool(isRunningHash, true);
        }
    }

    private void HandleCombat() {
        float distance = Vector3.Distance(player.position, gameObject.transform.position);
        if (distance < 1.5f && canAttack) { 
            canAttack = false;
            animator.SetTrigger(isAttackingHash);
            StartCoroutine(ResetAttackCooldown(1.0f));
        }
    }

    private IEnumerator ResetAttackCooldown(float cooldown) { 
        yield return new WaitForSeconds(cooldown);
        animator.ResetTrigger(isAttackingHash);
        canAttack = true;
    }

}
