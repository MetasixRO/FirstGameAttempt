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
    private float attackDelay = 0.0f;

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

    public void SetDelay(float delay) {
        attackDelay = delay;
    }

    public float GetDelay() { 
        return attackDelay;
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

    public virtual bool CheckForCombat() {
        float distance = Vector3.Distance(player.transform.position, gameObject.transform.position);

        if (distance <= 1.5f && canAttack && isLookingAtPlayer){
            return true;
        }
        return false;
        
    }

    public virtual void HandleCombat() {
            canAttack = false;

            animator.SetTrigger(isAttackingHash);
            StartCoroutine(ResetAttackCooldown(attackDelay));
    }

    protected IEnumerator ResetAttackCooldown(float cooldown) { 
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
            Vector3 directionToPlayer = (player.transform.position - transform.position).normalized;

            Vector3 forward = transform.forward;

            float dotProduct = Vector3.Dot(forward, directionToPlayer);

                if (dotProduct > 0.8f)
                {
                    SetLookingAtPlayer(true);
                }
                else
                {
                    SetLookingAtPlayer(false);
                    
                    directionToPlayer.y = 0;

                    Quaternion lookLocation = Quaternion.LookRotation(directionToPlayer);

                    transform.rotation = Quaternion.Lerp(transform.rotation, lookLocation, 2.5f * Time.deltaTime);
                }
            
        }
    }

    public void HandlePunched()
    {
        animator.SetTrigger("Hurt");
    }

    private void SetDead()
    {
        animator.SetBool("isDead", true);
    }

    public void HandleDeath() {
        animator.SetTrigger("Die");
    }

    protected Animator GetAnimator() {
        return animator;
    }

    protected void SetAgent(NavMeshAgent agent) {
        this.agent = agent;
        player = PlayerTracker.instance.player;
    }

    protected void SetAnimator(Animator animator) {
        this.animator = animator;
        GetHashes();
    }

    protected GameObject GetPlayer() {
        return player;
    }

    protected void SetAttackHash(int attackHash) {
        this.isAttackingHash = attackHash;
    }

    protected bool CheckCanAttack() {
        return canAttack;
    }

    protected void SetCanAttack(bool canAttack) {
        this.canAttack = canAttack;
    }
}
