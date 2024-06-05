using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GreatSwordSpecialHelper : MonoBehaviour
{
    private SphereCollider currentCollider;
    private float damage;
    void Start()
    {
        damage = 0;
        currentCollider = GetComponent<SphereCollider>();
        currentCollider.enabled = false;

        GreatSwordSpecial.ActivateGreatSwordSpecial += Activate;
        GreatSwordSpecial.DeactivateGreatSwordSpecial += Deactivate;
    }

    private void Activate(float range, float damage) {
        currentCollider.radius = range;
        currentCollider.enabled = true;
        this.damage = damage;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Enemy")) { 
            other.GetComponent<EnemyCombat>().TakeDamage(damage);
            other.GetComponent<Controller>().Freeze();
            other.GetComponent<AgentAnimations>().Freeze();
            other.GetComponent<EnemyStateManager>().TransitionToFreeze();

            Vector3 knockbackDirection = other.transform.position - PlayerTracker.instance.player.transform.position;
            knockbackDirection.Normalize();

            other.transform.position += knockbackDirection * 100f * Time.deltaTime;
        }
    }

    private void Deactivate(float range, float damage) {
        currentCollider.enabled = false;
    }
}
