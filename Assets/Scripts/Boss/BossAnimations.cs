using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class BossAnimations : AgentAnimations
{
    public delegate void BossAnimationsEvent();
    public static event BossAnimationsEvent AttackDone;
    public static event BossAnimationsEvent MeleeLaunched;
    public static event BossAnimationsEvent AreaLaunched;
    public static event BossAnimationsEvent RangeLaunched;

    public delegate void BossPositionEvent(GameObject boss);
    public static event BossPositionEvent CurrentPosition;

    private int meleeHash, areaHash, rangeHash, attackHash;
    private int attackType;
    private bool canMelee, canArea, canRange;
    private float meleeDelay, areaDelay, rangeDelay;

    private LayerMask layerToIgnore;
    int layerMask;

    private void Start()
    {
        layerToIgnore = LayerMask.GetMask("Enemy");
        layerMask = Physics.DefaultRaycastLayers & ~layerToIgnore.value;

        attackType = 0;

        canMelee = true;
        canArea = true;
        canRange = true;

        base.SetAgent(GetComponent<NavMeshAgent>());
        base.SetAnimator(GetComponent<Animator>());

        attackHash = Animator.StringToHash("Attack");
        meleeHash = Animator.StringToHash("Close");
        areaHash = Animator.StringToHash("Area");
        rangeHash = Animator.StringToHash("Range");
        base.SetCanAttack(true);

        BossController.AttackChoice += SetAttackType;
    }

    private void SetAttackType(int attackChoice) {
        attackType = attackChoice;
    }

    public override bool CheckForCombat() {
        
        bool result = false;
        float distance = Vector3.Distance(base.GetPlayer().transform.position, gameObject.transform.position);

        switch (attackType) {
            case 0: break;
            case 1: if (distance < 1.4f && canMelee && CheckLookingAtPlayer()) { result = true; } else { result = false; } break;
            case 2: if (distance < 5.0f && canArea && CheckCanSeePlayer(distance)) { result = true; } else { result = false; } break;
            case 3: if (canRange && CheckCanSeePlayer(distance)) { result = true; } else { result = false; } break;
        }
        return result;
    }

    public override void HandleCombat()
    {
        base.GetAnimator().SetTrigger(attackHash);
        switch (attackType) {
            case 1: 
                canMelee = false; 
                base.GetAnimator().SetTrigger(meleeHash);
                StartCoroutine(ResetCooldown(1,meleeDelay));
                break;
            case 2:
                canArea = false;
                base.GetAnimator().SetTrigger(areaHash);
                StartCoroutine(ResetCooldown(2, areaDelay));
                break;
            case 3:
                canRange = false;
                base.GetAnimator().SetTrigger(rangeHash);
                StartCoroutine(ResetCooldown(3, rangeDelay));
                break;
        }
    }

    private bool CheckLookingAtPlayer() {
        Vector3 directionToPlayer = (base.GetPlayer().transform.position - transform.position).normalized;

        Vector3 forward = transform.forward;

        float dotProduct = Vector3.Dot(forward, directionToPlayer);

        return dotProduct > 0.7f;
    }

    private bool CheckCanSeePlayer(float maxDistance)
    {
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
        else
        {
            return true;
        }
        return false;
    }

    private IEnumerator ResetCooldown(int attackParameter, float timer) {
        yield return new WaitForSeconds(timer);
        switch (attackParameter) {
            case 1: canMelee = true; break;
            case 2: canArea = true; break;
            case 3: canRange = true; break;
        }
        AttackDone?.Invoke();
        Debug.Log("ResetParameter");
    }

    public void SetDelay(float melee, float area, float range) {
        Debug.Log(melee);
        meleeDelay = melee;
        areaDelay = area;
        rangeDelay = range;
    }

    public void LaunchAttackEvent(int attackType) {
        switch (attackType) {
            case 1: MeleeLaunched?.Invoke(); break;
            case 2: CurrentPosition?.Invoke(gameObject); AreaLaunched?.Invoke(); break;
            case 3: if (RangeLaunched != null) { Debug.Log("yes"); } RangeLaunched?.Invoke(); break;
        }
    }
}
