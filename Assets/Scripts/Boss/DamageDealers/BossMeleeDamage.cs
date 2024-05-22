using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossMeleeDamage : MonoBehaviour
{
    public delegate void BossMelee(float damage);
    public static event BossMelee BossDamage;

    private SphereCollider currentCollider;
    private float damage;

    private void Awake()
    {
        BossCombat.MeleeDamage += SetDamage;
        BossAnimations.MeleeLaunched += ManageCollider;
    }

    private void Start()
    {
        currentCollider = GetComponent<SphereCollider>();
        currentCollider.enabled = false;
    }

    private void SetDamage(float damage) {
        this.damage = damage;
    }

    public void ManageCollider() { 
        currentCollider.enabled = !currentCollider.enabled;
    }

    private void OnTriggerEnter(Collider other)
    {
        Debug.Log("Copllider " + currentCollider.enabled);
        Debug.Log("Tag " + other.tag);
        if (currentCollider.enabled && other.CompareTag("Player")) {
            BossDamage?.Invoke(damage);
        }
    }
}
