using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class MenuElementManager : MonoBehaviour
{
    public delegate void MirrorObjectClicked(string description);
    public static event MirrorObjectClicked DisplayMirrorAbilityDescription;

    public delegate void CheckEnoughCoins(int amount);
    public static event CheckEnoughCoins CheckIfCanBuyMirrorAbility;

    private MirrorAbilityBase ability;
    private TextMeshProUGUI abilityName;
    private TextMeshProUGUI currentBonus;
    private TextMeshProUGUI cost;

    private bool canPurchase = false;

    public void SetAbility(MirrorAbilityBase ability) {
        ResourceManager.CheckCoinsResult += SetCanPurchase;
        this.ability = ability;
        ability.RememberOriginalStats();
        UpdateElements();
    }

    public void UpdateElements() { 
        TextMeshProUGUI[] elements = GetComponentsInChildren<TextMeshProUGUI>();
        abilityName = elements[0];
        currentBonus = elements[1];
        cost = elements[2];

        DisplayStats();
    }

    public void DisplayDescription() {
        if (DisplayMirrorAbilityDescription != null) {
            DisplayMirrorAbilityDescription(ability.description);
        }
    }

    public void PurchaseRank() {
        //CHECK COINS

        if (CheckIfCanBuyMirrorAbility != null) {
            CheckIfCanBuyMirrorAbility(ability.GetCost());
        }

        if (ability.GetRank() < ability.maxRank && canPurchase)
        {
            ability.IncreaseCurrentBonus();
            ability.IncreasePrice();
            ability.IncreaseRank();

            canPurchase = false;

            DisplayStats();
        }
    }

    public void CheckIfShouldDisableButton(Button button) {
        if (ability.GetRank() == ability.maxRank) {
            button.interactable = false;
        }
    }

    private void DisplayStats() {
        cost.text = ability.GetCost().ToString();
        abilityName.text = ability.GetName();
        currentBonus.text = ability.GetBonus().ToString();
    }

    private void SetCanPurchase(bool result) {
        canPurchase = result;
    }
}
