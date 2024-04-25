using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class DashAnimations : AgentAnimations
{
    private int dashHash;
    private EnemyDash dashManager;
    private LayerMask layerToIgnore;
    int layerMask;

    private void Start()
    {
        layerToIgnore = LayerMask.GetMask("Enemy");
        layerMask = Physics.DefaultRaycastLayers & ~layerToIgnore.value;
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
            if (CheckCanSeePlayer(distance))
            {
                return true;
            }
        }
        return false;
    }

    public override void HandleCombat() {
        base.SetCanAttack(false);
        base.GetAnimator().SetTrigger(dashHash);
        StartCoroutine(base.ResetAttackCooldown(base.GetDelay()));
        dashManager.InitiateDash();
    }

    private bool CheckCanSeePlayer(float maxDistance) {
        Vector3 direction = (base.GetPlayer().transform.position - gameObject.transform.position).normalized;

        Ray ray = new Ray(gameObject.transform.position, direction);

        RaycastHit hit;
        if (Physics.Raycast(ray, out hit, maxDistance, layerMask))
        {
            if (hit.collider.gameObject == base.GetPlayer())
            {
                return true;
            }
        }
        else {
            return true;
        }
        return false;
    }


}
