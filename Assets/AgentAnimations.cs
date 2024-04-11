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

    private EnemyDealDamage damageDealerManager;

    private bool canAttack,isLookingAtPlayer;

    private GameObject player;

    void Start()
    {
        agent = GetComponent<NavMeshAgent>();
        animator = GetComponent<Animator>();
        damageDealerManager = GetComponentInChildren<EnemyDealDamage>();
        GetHashes();
        player = PlayerTracker.instance.player;
        
        canAttack = true;
        isLookingAtPlayer = false;
    }

    public void HandleMovement() {
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

    private void DisableWeaponDamageAbility() {
        if (damageDealerManager != null)
        {
            damageDealerManager.ManageWeaponDamageDealing(false);
        }
    }

    private void ActivateWeaponDamageAbility() {
        if (damageDealerManager != null)
        {
            damageDealerManager.ManageWeaponDamageDealing(true);
        }
    }

    public bool CheckForCombat() {
        float distance = Vector3.Distance(player.transform.position, gameObject.transform.position);

        if (distance <= 1.5f && canAttack && isLookingAtPlayer){
            return true;
        }
        return false;
        
    }

    public void HandleCombat() {
            canAttack = false;

            animator.SetTrigger(isAttackingHash);
            StartCoroutine(ResetAttackCooldown(2.0f));
    }

    private IEnumerator ResetAttackCooldown(float cooldown) { 
        yield return new WaitForSeconds(cooldown);
        animator.ResetTrigger(isAttackingHash);

        canAttack = true;
    }

    public void SetLookingAtPlayer(bool value) {
        isLookingAtPlayer = value;
    }

    private void GetHashes() {
        isRunningHash = Animator.StringToHash("Running");
        isAttackingHash = Animator.StringToHash("Punch");
    }

    public void Freeze() {
        animator.SetBool(isRunningHash, false);
    }

    public void Rotate() {
        if (agent.velocity.magnitude == 0.0)
        {
            Vector3 rayDirection = transform.forward;
            Ray ray = new Ray(transform.position, rayDirection);
            RaycastHit hit;

            if (Physics.Raycast(ray, out hit))
            {
                if (hit.collider == null)
                {
                    return;
                }

                if (hit.collider.gameObject == player)
                {
                    SetLookingAtPlayer(true);
                }
                else
                {
                    SetLookingAtPlayer(false);

                    Vector3 directionToPlayer = (player.transform.position - transform.position).normalized;

                    Quaternion lookLocation = Quaternion.LookRotation(new Vector3(directionToPlayer.x, 0, directionToPlayer.z));

                    transform.rotation = Quaternion.Lerp(transform.rotation, lookLocation, 2.5f * Time.deltaTime);
                }
            }
        }
    }
}
