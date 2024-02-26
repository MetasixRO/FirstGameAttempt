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

    private void Start()
    {
        target = PlayerTracker.instance.player.transform;
        animator = GetComponent<Animator>();
        cooldown = 1f;
        canAttack = true;
    }

    private void Update()
    {
        HandleRotation();
        HandleCombat(); 
    }

    private void HandleCombat() {
        float distance = Vector3.Distance(target.position, transform.position);
        if (distance <= radius && canAttack)
        {
            canAttack = false;
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
        canAttack = true;
    }
}
