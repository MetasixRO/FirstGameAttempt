using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoonAbilitiesContainer : MonoBehaviour
{
    public List<AbilityScriptableObject> abilities = new List<AbilityScriptableObject>();

    private void Start()
    {
        PersonalAbilityHolder.LevelReached += AddAbilityToTheBoon;
    }

    public List<AbilityScriptableObject> RetrieveAbility() {
        List<int> indexes = new List<int>();

        while (indexes.Count < 3) {
            int index = Random.Range(0, abilities.Count);

            if (!indexes.Contains(index)) {
                indexes.Add(index);
            }
        }

        List<AbilityScriptableObject> abilityList = new List<AbilityScriptableObject>();
        foreach (int index in indexes) {
            abilityList.Add(abilities[index]);
        }

        return abilityList;
    }

    private void AddAbilityToTheBoon(AbilityScriptableObject newAbility) {
        abilities.Add(newAbility);
    }
}
