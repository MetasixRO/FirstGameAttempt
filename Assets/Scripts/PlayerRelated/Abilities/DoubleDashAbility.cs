using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Double Dash", menuName = "Ability/Double Dash")]
public class DoubleDashAbility : AbilityScriptableObject
{
    public delegate void DoubleDashEvent();
    public static event DoubleDashEvent ReduceTime;
    public static event DoubleDashEvent ResetTime;

    public override void Activate()
    {
        isActive = true;
        if (ReduceTime != null) {
            ReduceTime();
        }
    }

    public override void Disable()
    {
        isActive = false;
        if (ResetTime != null)
        {
            ResetTime();
        }
    }
}
