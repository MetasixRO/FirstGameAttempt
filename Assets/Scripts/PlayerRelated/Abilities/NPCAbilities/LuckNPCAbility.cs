using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Luck", menuName = "Ability/Luck")]
public class LuckNPCAbility : AbilityScriptableObject
{
    public override void Activate()
    {
        Debug.Log("Yep, this NPC ability should insta kill some of the enemies");
    }

    public override void Disable()
    {
        Debug.Log("It's over");
    }
}
