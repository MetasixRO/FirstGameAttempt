using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyCombat : MonoBehaviour
{

    private bool canReceiveDamage;
    private float damageReceived;

    void Start()
    {
        canReceiveDamage = false;
        damageReceived = 0;
    }

    public void SetCanReceiveDamage(float damageAmount) { 
        canReceiveDamage=true;
        if (damageReceived == 0) { 
            damageReceived = damageAmount;
        }
    }

    public void ClearCanReceiveDamage() {
        canReceiveDamage = false;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("PlayerWeapon") && canReceiveDamage) {
            GetComponent<Enemy>().TakeDamage(damageReceived);
        }
    }
}
