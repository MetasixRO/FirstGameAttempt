using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponPrompt : MonoBehaviour, IInteractable
{
    public delegate void SelectedWeapon();
    public static event SelectedWeapon WeaponSelected;

    public delegate void SelectWeapon(int weaponID);
    public static event SelectWeapon ChangeWeapon;
    public static event SelectWeapon CheckOwnedWeapon;
    public static event SelectWeapon PurchaseWeapon;

    public static event SelectWeapon CheckEnoughCredits;

    public delegate void UpdateStats(float damage, float cooldown, float specialDamage, float specialCooldown);
    public static event UpdateStats ChangeWeaponStats;

    public delegate void ChangeSpecial(SpecialAttack special);
    public static event ChangeSpecial SpecialAttackInside;

    private string prompt;
    private string lockPrompt;
    private bool isPurchased = false;
    private bool purchaseReult = false;
    private int weaponNumber;
    Canvas lockCanvas;
    private WeaponStats weaponStatsComponent;

    private void Start()
    {
        lockCanvas = GetComponentInChildren<Canvas>();
        Weapons.ResultForPurchaseCheck += UpdateIsPurchased;
        ResourceManager.CheckKeysResult += GetPurchaseStatus;
        weaponStatsComponent = GetComponent<WeaponStats>();
        if (weaponStatsComponent != null)
        {
            weaponNumber = weaponStatsComponent.GetWeaponID();
            prompt = weaponStatsComponent.GetWeaponName();
            lockPrompt = "(E) Buy " + prompt;
        }
    }

    private void UpdateIsPurchased(bool status) {
        isPurchased = status;
    }

    private void GetPurchaseStatus(bool status) {
        purchaseReult = status;
    }

    public string InteractionPrompt()
    {
        if (CheckOwnedWeapon != null) { 
            CheckOwnedWeapon(weaponNumber);
        }

        if (isPurchased)
        {
            return prompt;
        }
        else
        {
            return lockPrompt;
        }
    }

    public void Interact()
    {
        if (!isPurchased)
        {
            if (CheckEnoughCredits != null) {
                CheckEnoughCredits(5);
            }

            if (purchaseReult)
            {
                if (PurchaseWeapon != null)
                {
                    PurchaseWeapon(weaponNumber);
                    lockCanvas.enabled = false;
                }

                UpdateIsPurchased(purchaseReult);
            }
            else {
                lockCanvas.GetComponent<Animator>().SetTrigger("Fail");
            }
        }
        else
        {

            if (ChangeWeapon != null && ChangeWeaponStats != null && SpecialAttackInside != null)
            {
                WeaponSelected?.Invoke();
                ChangeWeapon(weaponNumber);
                ChangeWeaponStats(weaponStatsComponent.GetWeaponDamage(), weaponStatsComponent.GetWeaponCooldown(), weaponStatsComponent.GetWeaponSpecialDamage(), weaponStatsComponent.GetWeaponSpecialCooldown());
                SpecialAttackInside(weaponStatsComponent.GetSpecialAttack());
            }
        }
    }
}
