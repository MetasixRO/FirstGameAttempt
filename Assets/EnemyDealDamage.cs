using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyDealDamage : MonoBehaviour
{
    public delegate void EnemyDealDamageEvent(float damage);
    public static event EnemyDealDamageEvent PlayerReceiveDamage;

    private float attackDamage;

    private BoxCollider currentCollider;

    private void Start()
    {
        attackDamage = 0;
        gameObject.TryGetComponent<BoxCollider>(out currentCollider);
        if (currentCollider != null)
        {
            currentCollider.enabled = false;
        }
    }

    public virtual void SetDamage(float damage) {
        if (attackDamage == 0) {
            attackDamage = damage;
        }
    }

    public virtual void ManageWeaponDamageDealing(bool activate) {

        if (activate)
        {
            if (currentCollider != null)
            {
                currentCollider.enabled = true;
            }
        }
        else
        {
            if (currentCollider != null)
            {
                currentCollider.enabled = false;
            }
        }
    }

    public virtual void DisableWeapon() {
        if (currentCollider != null)
        {
            currentCollider.enabled = false;
        }
    }


    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player") || other.CompareTag("PlayerWeapon")) {
            if (PlayerReceiveDamage != null) {
                PlayerReceiveDamage(attackDamage);
            }
        }
    }
}
