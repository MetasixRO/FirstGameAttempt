using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Great Sword Special", menuName = "Special/Great Sword Special")]
public class GreatSwordSpecial : SpecialAttack
{
    private float range = 12.5f;
    private int numFound = 0;
    private LayerMask layer;
    private readonly Collider[] colliders = new Collider[15];
    private float knockbackForce = 250f;

    public override void Activate(float damage) {
        if (layer != null)
        {
            numFound = Physics.OverlapSphereNonAlloc(PlayerTracker.instance.player.transform.position, range, colliders, layer);
            if (numFound > 0)
            {
                for (int i = 0; i < numFound; i+=2) {
                    colliders[i].gameObject.GetComponent<EnemyCombat>().TakeDamage(damage);
                    Vector3 knockbackDirection = colliders[i].gameObject.transform.position - PlayerTracker.instance.player.transform.position;
                    knockbackDirection.Normalize();

                    colliders[i].gameObject.transform.position += knockbackDirection * knockbackForce * Time.deltaTime;
                }
            }
        }
        else {
            Debug.Log("Layer not set");
        }
    }

    public override void Deactivate() {
        Debug.Log("Deactivated");
    }

    private void OnEnable()
    {
        layer = LayerMask.GetMask("Enemy");
    }
}
