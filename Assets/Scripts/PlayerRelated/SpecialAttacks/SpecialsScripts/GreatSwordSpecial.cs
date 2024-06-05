using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Great Sword Special", menuName = "Special/Great Sword Special")]
public class GreatSwordSpecial : SpecialAttack
{
    public delegate void GreatSwordSpecialEvent(float range, float damage);
    public static event GreatSwordSpecialEvent ActivateGreatSwordSpecial;
    public static event GreatSwordSpecialEvent DeactivateGreatSwordSpecial;

    private float range = 1.5f;

    public override void Activate(float damage) {
        ActivateGreatSwordSpecial?.Invoke(1.5f, damage);
    }

    public override void Deactivate() {
        DeactivateGreatSwordSpecial?.Invoke(range, 0);
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(PlayerTracker.instance.player.transform.position, range);
    }

}
