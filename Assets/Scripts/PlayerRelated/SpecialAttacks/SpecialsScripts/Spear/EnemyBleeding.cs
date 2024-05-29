using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyBleeding : MonoBehaviour
{
    private float damageReceived;
    private EnemyCombat combatComponent;
    private bool shouldBleed = false;

    private bool isBleeding = false;

    public void ApplyBleeding(float damage) {
        damageReceived = damage;
        shouldBleed = true;
        if (combatComponent == null) { 
            combatComponent = GetComponent<EnemyCombat>();
        }
        if (!isBleeding)
        {
            StartCoroutine(Bleeding());
            isBleeding = true;
        }
        StartCoroutine(DelayStopBleeding());
    }

    public void DisableBleeding() {
        shouldBleed = false;
        isBleeding = false;
    }

    private IEnumerator Bleeding() {
        if (shouldBleed)
        {
            yield return new WaitForSeconds(1.0f);
            if (combatComponent.isAlive())
            {
                combatComponent.TakeDamage(damageReceived);
                StartCoroutine(Bleeding());
            }
        }
    }

    private IEnumerator DelayStopBleeding() {
        yield return new WaitForSeconds(5.0f);
        DisableBleeding();
    }


}
