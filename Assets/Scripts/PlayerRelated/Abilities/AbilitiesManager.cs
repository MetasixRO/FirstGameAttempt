using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AbilitiesManager : MonoBehaviour
{
    public delegate void AbilityTypeAnswer(bool answer);
    public static event AbilityTypeAnswer CheckAnswer;

    public delegate void AbilityNumberEvent(AbilityScriptableObject ability, List<AbilityScriptableObject> abilities);
    public static event AbilityNumberEvent TooManyAbilities;

    [SerializeField] private List<AbilityScriptableObject> abilities = new List<AbilityScriptableObject>();

    private void Start()
    {
        Combat.PlayerDead += ResetAbilities;
        BoonMenuManager.AbilitySelected += AddAbility;
        BoonAbilitiesContainer.CheckOwnedAbility += CheckOwned;
        BoonMenuManager.ReplacementSelected += ReplaceAbility;
        UpdatedStateManager.CallSpecial += CheckAndCallSpecial;


        foreach (AbilityScriptableObject ability in abilities) {
            ability.Activate();
        }
    }

    public void ActivateAbility() {
        abilities[abilities.Count - 1].Activate();
    }

    IEnumerator DisableAfterDelay(AbilityScriptableObject ability) {
        yield return new WaitForSeconds(ability.activeTime);
        ability.Disable();
        abilities.Remove(ability);
    } 

    public void AddAbility(AbilityScriptableObject newAbility) {
        if (abilities.Count == 3 && newAbility.abilityType != AbilityType.InstantRewards)
        {
            TooManyAbilities?.Invoke(newAbility, abilities);
        }
        else
        {

            if (newAbility.abilityType == AbilityType.InstantRewards)
            {
                newAbility.Activate();
                
            }
            else if (newAbility.abilityType == AbilityType.NPCSpecial)
            {
                abilities.Add(newAbility);
                
            }
            else {
                abilities.Add(newAbility);
                ActivateAbility();
            }
        }
        
    }

    private int FindAbilityIndex(AbilityScriptableObject ability) {
        for (int i = 0; i < 3; i++) {
            if (abilities[i] == ability) {
                return i;
            }
        }
        return 0;
    }

    private void ReplaceAbility(AbilityScriptableObject newAbility, AbilityScriptableObject oldAbility) {
        abilities[FindAbilityIndex(oldAbility)].Disable();
        abilities.Remove(oldAbility);
        abilities.Add(newAbility);
        if (newAbility.abilityType != AbilityType.NPCSpecial)
        {
            newAbility.Activate();
        }
    }

    public void ResetAbilities() {
        StartCoroutine(DelayResetAbilities(1.0f));    }

    private void CheckOwned(AbilityScriptableObject newAbility) {
        bool owned = false;
        foreach(var ability in abilities) {
            if (ability.abilityType == newAbility.abilityType) {
                owned = true;
                break;
            }
        }
        if (CheckAnswer != null)
        {
            CheckAnswer(owned);
        }
    }

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

    private void CheckAndCallSpecial() {
        Debug.Log("Called Special");
        foreach (var ability in abilities) {
            if (ability.abilityType == AbilityType.NPCSpecial) {
                Debug.Log("Found a Special");
                ability.Activate();
                StartCoroutine(DisableAfterDelay(ability));
            }
        }
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
