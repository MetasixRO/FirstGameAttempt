using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DashDealDamage : EnemyDealDamage
{
    public new static event EnemyDealDamageEvent PlayerReceiveDamage;
    private Collider enemyCollider;
    private float damage;

    private void Start()
    {
        damage = 0;
    }

    public override void SetDamage(float damage)
    {
        this.damage = damage;
        enemyCollider = GetComponent<Collider>();
    }

    public override void ManageWeaponDamageDealing(bool activate)
    {
        enemyCollider.isTrigger = activate;
    }

    public override void DisableWeapon()
    {
        enemyCollider.isTrigger = false;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (enemyCollider.isTrigger && other.CompareTag("Player")) {
            if (PlayerReceiveDamage != null) {
                PlayerReceiveDamage(damage);
            }
        }
    }
}
