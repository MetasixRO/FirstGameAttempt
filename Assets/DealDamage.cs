using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DealDamage : MonoBehaviour
{
    private bool canDealDamage;
    private float attackDamage;

    private void Start()
    {
        attackDamage = 0;
        canDealDamage = false;
        Combat.ManageWeapon += ManageWeaponDamageDealing;
    }

    private void ManageWeaponDamageDealing(float cooldown, float damage) {
        if (attackDamage == 0) {
            attackDamage = damage;
        }

        if (!canDealDamage)
        {
            canDealDamage = true;
        }
        else {
            canDealDamage = false;
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (canDealDamage && other.CompareTag("Enemy")) {
            other.GetComponent<EnemyCombat>().TakeDamage(attackDamage);
        }
    }




}
