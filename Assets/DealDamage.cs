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

        DoubleDamage.DamageDouble += DoubleCurrentDamage;
        DoubleDamage.DamageReset += ResetCurrentDamage;

        KnifeSpecial.BoostStats += DoubleCurrentDamage;
        KnifeSpecial.ResetStats += ResetCurrentDamage;
    }

    private void ManageWeaponDamageDealing(float damage) {
        if (gameObject.activeSelf)
        {
            if (attackDamage == 0)
            {
                attackDamage = damage;
            }
            Debug.Log(attackDamage);

            if (!canDealDamage)
            {
                if (!isDashing)
                {
                    canDealDamage = true;
                }
            }
            else
            {
                canDealDamage = false;
            }
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (canDealDamage && other.CompareTag("Enemy") && gameObject.activeSelf) {
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


    private void DoubleCurrentDamage() {
        attackDamage *= 2;
        //Debug.Log("" + attackDamage);
    }

    private void ResetCurrentDamage() { 
        attackDamage /= 2;
    }

}
