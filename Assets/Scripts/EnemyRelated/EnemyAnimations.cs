using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemyAnimations : AgentAnimations
{
    private int shootHash;
    private Gun gunManager;

    private void Start()
    {
        
        base.SetAgent(GetComponent<NavMeshAgent>());
        base.SetAnimator(GetComponent<Animator>());

        base.SetCanAttack(true);

        shootHash = Animator.StringToHash("Shoot");
        base.SetAttackHash(shootHash);

        gunManager = GetComponentInChildren<Gun>();
    }

    public override bool CheckForCombat() {
        float distance = Vector3.Distance(base.GetPlayer().transform.position, gameObject.transform.position);

        if (distance <= 8.5f && base.CheckCanAttack())
        {
            return true;
        }
        return false;
    }

    public override void HandleCombat()
    {
        base.SetCanAttack(false);
        base.GetAnimator().SetTrigger(shootHash);
        StartCoroutine(base.ResetAttackCooldown(base.GetDelay()));
        if (gunManager != null) {
            //Debug.Log("I am now shooting");
            gunManager.Shoot();
        }
    }
}
