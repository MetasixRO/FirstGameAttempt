using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyDealDamage : MonoBehaviour
{
    public delegate void EnemyDealDamageEvent(float damage);
    public static event EnemyDealDamageEvent PlayerReceiveDamage;

    private float attackDamage;
    private bool canDealDamage;

    private void Start()
    {
        attackDamage = 0;
        canDealDamage = false;
    }

    public void SetDamage(float damage) {
        if (attackDamage == 0) {
            attackDamage = damage;
        }
    }

    public void ManageWeaponDamageDealing(bool activate) {

        if (activate)
        {
            canDealDamage = true;
        }
        else
        {
            canDealDamage = false;
        }
    }

    public void DisableWeapon() {
        gameObject.GetComponent<Collider>().enabled = false;
    }


    private void OnTriggerEnter(Collider other)
    {
        if (canDealDamage && other.CompareTag("Player")) {
            if (PlayerReceiveDamage != null) {
                PlayerReceiveDamage(attackDamage);
            }
        }
    }
}
