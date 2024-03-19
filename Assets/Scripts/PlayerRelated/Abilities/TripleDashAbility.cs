using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Triple Dash", menuName = "Ability/Triple Dash")]
public class TripleDashAbility : AbilityScriptableObject
{
    public delegate void TripleDashEvent();
    public static event TripleDashEvent ReduceTime;
    public static event TripleDashEvent ResetTime;

    public override void Activate()
    {
        isActive = true;
        if (ReduceTime != null)
        {
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
