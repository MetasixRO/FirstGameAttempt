using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponPrompt : MonoBehaviour, IInteractable
{

    public delegate void SelectWeapon(int weaponID);
    public static event SelectWeapon ChangeWeapon;

    public delegate void UpdateStats(float damage, float cooldown, float specialDamage, float specialCooldown);
    public static event UpdateStats ChangeWeaponStats;

    public delegate void ChangeSpecial(SpecialAttack special);
    public static event ChangeSpecial SpecialAttackInside;

    private string prompt;
    private int weaponNumber;
    private WeaponStats weaponStatsComponent;

    private void Start()
    {
        weaponStatsComponent = GetComponent<WeaponStats>();
        if (weaponStatsComponent != null)
        {
            weaponNumber = weaponStatsComponent.GetWeaponID();
            prompt = weaponStatsComponent.GetWeaponName();
        }
    }

    public string InteractionPrompt()
    {
        return prompt;
    }

    public void Interact()
    {
        if (ChangeWeapon != null && ChangeWeaponStats != null && SpecialAttackInside != null) {
            ChangeWeapon(weaponNumber);
            ChangeWeaponStats(weaponStatsComponent.GetWeaponDamage(), weaponStatsComponent.GetWeaponCooldown(), weaponStatsComponent.GetWeaponSpecialDamage(), weaponStatsComponent.GetWeaponSpecialCooldown());
            SpecialAttackInside(weaponStatsComponent.GetSpecialAttack());
        }
    }
}
