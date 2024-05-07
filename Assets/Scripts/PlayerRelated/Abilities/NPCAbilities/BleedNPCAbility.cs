using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Bleed", menuName = "Ability/Bleed")]
public class BleedNPCAbility : AbilityScriptableObject
{
    public override void Activate()
    {
        Debug.Log("Yep, this NPC ability should add bleeding effect to all the enemies");
    }

    public override void Disable()
    {
        Debug.Log("It's over");
    }
}
