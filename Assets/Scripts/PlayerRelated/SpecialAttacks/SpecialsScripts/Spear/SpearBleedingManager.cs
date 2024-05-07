using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpearBleedingManager : MonoBehaviour
{

    private bool applyBleeding;
    private float bleedingDamage;
    void Start()
    {
        applyBleeding = false;
        bleedingDamage = 0.0f;
        SpearSpecial.ApplyBleeding += SetApplyBleeding;
        SpearSpecial.DisableBleeding += SetDisableBleeding;
    }

    private void SetApplyBleeding(float damage) {
        applyBleeding = true;
        bleedingDamage = damage;
    }

    private void SetDisableBleeding(float damage) {
        applyBleeding = false;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Enemy") && applyBleeding) {
            other.GetComponent<EnemyBleeding>().ApplyBleeding(bleedingDamage);
        }
    }
}
