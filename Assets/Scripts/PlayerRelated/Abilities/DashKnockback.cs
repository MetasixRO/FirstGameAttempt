using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Dash Knockback", menuName = "Ability/Dash KnockBack")]
public class DashKnockback : AbilityScriptableObject
{
    public delegate void DashKnockbackEvent();
    public static event DashKnockbackEvent ApplyKnockback;
    public static event DashKnockbackEvent DisableKnockback;

    public override void Activate()
    {
        CharacterMovement.Dash += ActivateEffect;
        NewDash.DashDone += DisableEffect;
    }

    public override void Disable()
    {
        CharacterMovement.Dash -= ActivateEffect;
        NewDash.DashDone -= DisableEffect;
    }

    private void ActivateEffect() { 
        ApplyKnockback?.Invoke();
    }

    private void DisableEffect() {
        DisableKnockback?.Invoke();
    }
}
