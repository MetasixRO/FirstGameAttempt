using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PersonalAbilityHolder : MonoBehaviour
{
    public delegate void NPCFriendshipEvent(AbilityScriptableObject ability);
    public static event NPCFriendshipEvent LevelReached;

    public AbilityScriptableObject ability;

    public void AddAbilityToTheBoon() {
        if (LevelReached != null) {
            LevelReached(ability);
        }
    }
}
