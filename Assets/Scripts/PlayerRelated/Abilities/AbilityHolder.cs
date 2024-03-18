using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AbilityHolder : MonoBehaviour
{
    public AbilityScriptableObject ability;
    float cooldown;
    float activeTime;

    private bool activate = false;

    enum AbilityState
    {
        waking,
        ready,
        active,
        cooldown
    }
    AbilityState state = AbilityState.waking;

    private void Update()
    {
        switch (state) {
            case AbilityState.waking:
                state = AbilityState.ready;
                CharacterMovement.Dash += () => { activate = true;};
                break;
            case AbilityState.ready:
                if (activate) {
                    ability.Activate();
                    activeTime = ability.activeTime;
                    state = AbilityState.active;
                }
                break;
            case AbilityState.active:
                if (activeTime > 0)
                {
                    activeTime -= Time.deltaTime;
                }
                else {
                    ability.Disable();
                    activate = false;
                    state = AbilityState.cooldown;
                    cooldown = ability.cooldown;
                }
                break;
            case AbilityState.cooldown:
                if (cooldown > 0)
                {
                    cooldown -= Time.deltaTime;
                }
                else {
                    state = AbilityState.ready;
                }
                break;
        }
    }
}
