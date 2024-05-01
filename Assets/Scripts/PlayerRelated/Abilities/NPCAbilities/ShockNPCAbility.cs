using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Shock", menuName = "Ability/Shock")]

public class ShockNPCAbility : AbilityScriptableObject
{
    public override void Activate()
    {
        Debug.Log("Yep, this NPC ability should shock and freeze all the enemies");
    }

    public override void Disable()
    {
        Debug.Log("It's over");
    }
}
