using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ActivateKnockbackArea : MonoBehaviour
{
    private Collider sphereCollider;

    void Start()
    {
        DashKnockback.ApplyKnockback += Activate;
        DashKnockback.DisableKnockback += Deactivate;
        sphereCollider = GetComponent<CapsuleCollider>();   
        sphereCollider.enabled = false;
    }

    private void Activate() {
        sphereCollider.enabled = true;
    }

    private void Deactivate() {
        sphereCollider.enabled = false;
    }

    private void OnTriggerEnter(Collider other)
    {
        Debug.Log("Entered");
        Debug.Log(other.tag);
        if (other.CompareTag("Enemy")) {
            other.GetComponent<Knockback>().ApplyKnockback();
            other.GetComponent<EnemyCombat>().TakeDamage(5);
        }
    }
}
