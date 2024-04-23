using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AbilitiesManager : MonoBehaviour
{
    private List<AbilityScriptableObject> abilities = new List<AbilityScriptableObject>();

    private void Start()
    {
        Combat.PlayerDead += ResetAbilities;
        BoonMenuManager.AbilitySelected += AddAbility;

        foreach (AbilityScriptableObject ability in abilities) {
            ability.Activate();
        }
    }

    public void ActivateAbility() {
        abilities[abilities.Count - 1].Activate();
    }

    public void AddAbility(AbilityScriptableObject newAbility) { 
        abilities.Add(newAbility);
        ActivateAbility();
    }

    public void ResetAbilities() {
        StartCoroutine(DelayResetAbilities(1.0f));    }

    private IEnumerator DelayResetAbilities(float delay) { 
        yield return new WaitForSeconds(delay);
        foreach (AbilityScriptableObject ability in abilities)
        {
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

    public List<AbilityScriptableObject> GetCurrentAbilities() {
        return abilities;
    }

    public string GetDescriptionByName(string name) {
        foreach (AbilityScriptableObject ability in abilities) {
            if (ability.name == name) {
                return ability.description;
            }
        }
        return "None";
    }

    public List<string> GetAllNames() { 
        List<string> names = new List<string> ();

        foreach (AbilityScriptableObject ability in abilities) { 
            names.Add (ability.name);
        }

        return names;
    }

    public int GetAbilityCount() {
        return abilities.Count;
    }

    public string GetDescriptionByIndex(int index) { 
        return abilities[index].description;
    }

    public string GetNameByIndex(int index) {
        return abilities[index].name;    
    }

    /*
    private void OnDisable()
    {
        foreach (AbilityScriptableObject ability in abilities)
        {
            Destroy(ability);
        }
    }
    */
}
