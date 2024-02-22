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
        float distance = Vector3.Distance(target.position, transform.position);
        if (distance <= radius && canAttack) {
            canAttack = false;
            animator.SetTrigger("Punch");
            StartCoroutine(AttackCooldown());
        }
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.blue;
        Gizmos.DrawWireSphere(transform.position, radius);
    }

    IEnumerator AttackCooldown() {
        yield return new WaitForSeconds(cooldown);
        canAttack = true;
    }
}
