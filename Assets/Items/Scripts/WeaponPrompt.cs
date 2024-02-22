using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponPrompt : MonoBehaviour, IInteractable
{

    public delegate void SelectWeapon(int weaponID);
    public static event SelectWeapon ChangeWeapon;

    public string prompt;
    private int weaponNumber;

    private void Start()
    {
        WeaponStats weaponStatsComponent = GetComponent<WeaponStats>();
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
        if (ChangeWeapon != null) {
            ChangeWeapon(weaponNumber);
        }
    }
}
