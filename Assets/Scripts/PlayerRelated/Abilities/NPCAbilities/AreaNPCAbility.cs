using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Area", menuName = "Ability/Area")]
public class AreaNPCAbility : AbilityScriptableObject
{
    public override void Activate()
    {
        Debug.Log("Yep, this NPC ability should damage all enemies around the player when he swings his weapon");
    }

    public override void Disable()
    {
        Debug.Log("It's over");
    }
}
