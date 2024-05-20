using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoonAbilitiesContainer : MonoBehaviour
{
    public delegate void BoonAbilitiesEvent(AbilityScriptableObject ability);
    public static event BoonAbilitiesEvent CheckOwnedAbility;

    private bool abilityOwned;

    public List<AbilityScriptableObject> abilities = new List<AbilityScriptableObject>();

    private void Start()
    {
        PersonalAbilityHolder.LevelReached += AddAbilityToTheBoon;
        AbilitiesManager.CheckAnswer += SetAbilityStatus;
    }

    public List<AbilityScriptableObject> RetrieveAbility() {
        List<int> indexes = new List<int>();
        List<AbilityType> types = new List<AbilityType>();

        while (indexes.Count < 3) {
            abilityOwned = true;
            int index = Random.Range(0, abilities.Count);

            if (CheckOwnedAbility != null) {
                CheckOwnedAbility(abilities[index]);
            }

            if (!indexes.Contains(index) && !types.Contains(abilities[index].abilityType) && !abilityOwned) {
                indexes.Add(index);
                types.Add(abilities[index].abilityType);
            }
        }

        List<AbilityScriptableObject> abilityList = new List<AbilityScriptableObject>();
        foreach (int index in indexes) {
            abilityList.Add(abilities[index]);
        }

        return abilityList;
    }

    private void SetAbilityStatus(bool status) {
        abilityOwned = status;
    }

    private void AddAbilityToTheBoon(AbilityScriptableObject newAbility) {
        abilities.Add(newAbility);
    }
}
