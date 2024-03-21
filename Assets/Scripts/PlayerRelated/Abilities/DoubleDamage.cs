using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Double Damage", menuName = "Ability/Double Damage")]
public class DoubleDamage : AbilityScriptableObject
{

    public delegate void DoubleDamageEvent();
    public static event DoubleDamageEvent DamageDouble;
    public static event DoubleDamageEvent DamageReset;

    public override void Activate()
    {
        if (DamageDouble != null) {
            DamageDouble();
        }
    }

    public override void Disable()
    {
        if (DamageReset != null) {
            DamageReset();
        }
    }
}
