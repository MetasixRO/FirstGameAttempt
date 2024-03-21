using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Double Max Health", menuName = "Ability/Double Max Health")]
public class DoubleMaxHealth : AbilityScriptableObject
{
    public delegate void DoubleMaxHealthEvent();
    public static event DoubleMaxHealthEvent DoubleHealth;
    public static event DoubleMaxHealthEvent ResetHealth;

    public override void Activate()
    {
        if (DoubleHealth != null) {
            DoubleHealth();
        }
    }

    public override void Disable()
    {
        if (ResetHealth != null) {
            ResetHealth();
        }
    }
}
