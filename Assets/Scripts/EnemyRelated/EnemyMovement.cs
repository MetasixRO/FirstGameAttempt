using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyMovement : MonoBehaviour
{
    private Transform target;
    public float radius = 1f;
    private bool canAttack;
    private float cooldown;
    private Animator animator;
    int isRunningHash;

    private EnemyDealDamage damageDealerManager;

    private void Start()
    {
        target = PlayerTracker.instance.player.transform;
        animator = GetComponent<Animator>();
        cooldown = 1f;
        canAttack = true;
        isRunningHash = Animator.StringToHash("Running");

        damageDealerManager = GetComponentInChildren<EnemyDealDamage>();
    }

    private void Update()
    {
        HandleMovement();
        HandleRotation();
        HandleCombat(); 
    }

    private void HandleMovement() {
        if (!animator.GetBool(isRunningHash)) { 
            animator.SetBool(isRunningHash, true);
        }
    }

    private void HandleCombat() {
        float distance = Vector3.Distance(target.position, transform.position);
        if (distance <= radius && canAttack)
        {
            canAttack = false;

            if (damageDealerManager != null)
            {
                //damageDealerManager.ManageWeaponDamageDealing();
            }

            animator.SetTrigger("Punch");
            StartCoroutine(AttackCooldown());
        }
    }

    private void HandleRotation() {
        float rotationSpeed = 5f;
        Vector3 newDirection = target.position - transform.position;
        newDirection.y = 0f;
        Quaternion targetRotation = Quaternion.LookRotation(newDirection);
        transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, rotationSpeed * Time.deltaTime);
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.blue;
        Gizmos.DrawWireSphere(transform.position, radius);
    }

    IEnumerator AttackCooldown() {
        yield return new WaitForSeconds(cooldown);
        animator.ResetTrigger("Punch");

        if (damageDealerManager != null)
        {
            //damageDealerManager.ManageWeaponDamageDealing();
        }

        canAttack = true;
    }
}
