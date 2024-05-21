using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Area", menuName = "Ability/Area")]
public class AreaNPCAbility : AbilityScriptableObject
{
    public delegate void AreaNPCAbilityEvent();
    public static event AreaNPCAbilityEvent SetAreaSignal;
    public static event AreaNPCAbilityEvent ResetAreaSignal;

    private bool enabled = false;

    public override void Activate()
    {
        Debug.Log("huh? " + enabled);
        if (!enabled) {
            SetAreaSignal?.Invoke();
            Debug.Log("Activating Area");
            enabled = true;
        }
    }

    public override void Disable()
    {
        if (enabled)
        {
            ResetAreaSignal?.Invoke();
            Debug.Log("It's over");
            enabled = false;
        }
    }
}
