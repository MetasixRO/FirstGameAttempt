using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoonAbilitiesContainer : MonoBehaviour
{
    public List<AbilityScriptableObject> abilities = new List<AbilityScriptableObject>();

    public AbilityScriptableObject RetrieveAbility() {
        int index = Random.Range(0, abilities.Count);

        return abilities[index];
    }
}
