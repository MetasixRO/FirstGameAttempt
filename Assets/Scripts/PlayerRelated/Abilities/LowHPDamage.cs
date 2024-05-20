using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Damage Resistance", menuName = "Ability/Damage Resistance")]
public class LowHPDamage : AbilityScriptableObject
{
    public delegate void LowHPDamageEvent();
    public static event LowHPDamageEvent ReduceDamage;

    public override void Activate()
    {
        Combat.CheckReduceReceivedDamage += Check;
    }

    public override void Disable()
    {
        Combat.CheckReduceReceivedDamage -= Check;
    }

    private void Check() {
        ReduceDamage?.Invoke();
    }
}
