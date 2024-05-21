using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Heal", menuName = "Ability/Heal")]
public class HealNPCAbility : AbilityScriptableObject
{
    public delegate void HealNPCAbilityEvent(int amount);
    public static event HealNPCAbilityEvent AddHealth;

    private bool enabled = false;

    public override void Activate()
    {
        if (!enabled) {
            AddHealth?.Invoke(25);
            Debug.Log("Adding Health");
            enabled = true;
        }
    }

    public override void Disable()
    {
        if (enabled) {
            Debug.Log("it's over");
            enabled = false;
        }
    }
}
