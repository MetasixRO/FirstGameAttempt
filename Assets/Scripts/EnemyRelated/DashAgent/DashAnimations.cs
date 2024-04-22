using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class DashAnimations : AgentAnimations
{
    private int dashHash;
    private EnemyDash dashManager;

    private void Start()
    {
        base.SetAgent(GetComponent<NavMeshAgent>());
        base.SetAnimator(GetComponent<Animator>());

        base.SetCanAttack(true);

        dashHash = Animator.StringToHash("Dash");
        base.SetAttackHash(dashHash);

        dashManager = GetComponent<EnemyDash>();
    }

    public override bool CheckForCombat()
    {
        float distance = Vector3.Distance(base.GetPlayer().transform.position, gameObject.transform.position);
        if (distance <= 7.5f && base.CheckCanAttack()) {
            return true;
        }
        return false;
    }

    public override void HandleCombat() {
        base.SetCanAttack(false);
        base.GetAnimator().SetTrigger(dashHash);
        StartCoroutine(base.ResetAttackCooldown(base.GetDelay()));
        dashManager.InitiateDash();
    }


}
