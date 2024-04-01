using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class AgentAnimations : MonoBehaviour
{
    public delegate void ManageAgentDestination();
    public static event ManageAgentDestination ManageDestination;

    private NavMeshAgent agent;
    private Animator animator;

    private int isRunningHash;
    private int isAttackingHash;

    private EnemyDealDamage damageDealerManager;

    private bool canAttack,isLookingAtPlayer;

    private Transform player;

    void Start()
    {
        agent = GetComponent<NavMeshAgent>();
        animator = GetComponent<Animator>();
        damageDealerManager = GetComponentInChildren<EnemyDealDamage>();
        GetHashes();
        player = PlayerTracker.instance.player.transform;
        
        canAttack = true;
        isLookingAtPlayer = false;
    }

    void Update()
    {
        HandleMovement();
        HandleCombat();
    }

    private void HandleMovement() {
        if (agent.velocity.magnitude > 0.1f)
        {
            if (!animator.GetBool(isRunningHash))
            {
                animator.SetBool(isRunningHash, true);
            }
        }
        else {
            if (animator.GetBool(isRunningHash))
            {
                animator.SetBool(isRunningHash, false);
            }
        }
    }

    private void ManageWeaponDamageAbility() {
        if (damageDealerManager != null)
        {
            damageDealerManager.ManageWeaponDamageDealing();
        }
    }

    private void HandleCombat() {
        float distance = Vector3.Distance(player.position, gameObject.transform.position);

        //Debug.Log(distance + " " + canAttack + " " + isLookingAtPlayer);
        if (distance <= 1.5f && canAttack && isLookingAtPlayer) {
            canAttack = false;

            if (ManageDestination != null) {
                ManageDestination();
            }

            animator.SetTrigger(isAttackingHash);
            StartCoroutine(ResetAttackCooldown(2.0f));
        }
    }

    private IEnumerator ResetAttackCooldown(float cooldown) { 
        yield return new WaitForSeconds(cooldown);
        animator.ResetTrigger(isAttackingHash);

        if (ManageDestination != null) {
            ManageDestination();
        }
        canAttack = true;
    }

    public void SetLookingAtPlayer(bool value) {
        isLookingAtPlayer = value;
    }

    private void GetHashes() {
        isRunningHash = Animator.StringToHash("Running");
        isAttackingHash = Animator.StringToHash("Punch");
    }
}
