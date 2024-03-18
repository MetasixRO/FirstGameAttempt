using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AbilitiesManager : MonoBehaviour
{
    private List<AbilityScriptableObject> abilities = new List<AbilityScriptableObject>();

    private void Start()
    {
        foreach (AbilityScriptableObject ability in abilities) {
            ability.Activate();
        }

        AddAbility(ScriptableObject.CreateInstance<DoubleDashAbility>());
    }

    public void ActivateAbility() {
        abilities[abilities.Count - 1].Activate();
    }

    public void AddAbility(AbilityScriptableObject newAbility) { 
        abilities.Add(newAbility);
        ActivateAbility();
    }

    public void ResetAbilities() {
        foreach (AbilityScriptableObject ability in abilities) {
            ability.Disable();
        }
        abilities.Clear();
    }

    public void ReplaceAbilityByType(AbilityScriptableObject newAbility) {
        foreach (AbilityScriptableObject ability in abilities) {
            if (ability.abilityType == newAbility.abilityType) {
                abilities[abilities.IndexOf(ability)] = newAbility;
                break;
            }
        }
    }

    private void OnDisable()
    {
        foreach (AbilityScriptableObject ability in abilities)
        {
            Destroy(ability);
        }
    }
}
