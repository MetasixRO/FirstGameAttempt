using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Luck", menuName = "Ability/Luck")]
public class LuckNPCAbility : AbilityScriptableObject
{
    public delegate void LuckNPCAbilityEvent(int counter);
    public static event LuckNPCAbilityEvent SendKillSignal;

    private bool enabled = false;
    public override void Activate()
    {
        if (!enabled) {
            int counter = Random.Range(1, 3);
            SendKillSignal?.Invoke(counter);
            enabled = true;
        }
    }

    public override void Disable()
    {
        if (enabled)
        {
            enabled = false;
        }
    }
}
