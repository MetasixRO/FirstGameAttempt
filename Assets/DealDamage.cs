using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DealDamage : MonoBehaviour
{
    private bool canDealDamage;
    private bool isDashing;
    private float attackDamage;

    private void Start()
    {
        attackDamage = 0;
        canDealDamage = false;
        isDashing = false;
        Combat.ManageWeapon += ManageWeaponDamageDealing;
        CharacterMovement.Dash += SetIsDashing;
        NewDash.DashDone += ResetIsDashing;
    }

    private void ManageWeaponDamageDealing(float cooldown, float damage) {
        if (attackDamage == 0) {
            attackDamage = damage;
        }

        if (!canDealDamage)
        {
            if (!isDashing)
            {
                canDealDamage = true;
            }
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

    private void SetIsDashing()
    {
        isDashing = true;
        canDealDamage = false;
    }

    private void ResetIsDashing() {
        isDashing = false;
    }




}
