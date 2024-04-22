using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Weapons : MonoBehaviour
{
    public delegate void WeaponCheckResult(bool purchased);
    public static event WeaponCheckResult ResultForPurchaseCheck;

    public delegate void WeaponPurchaseResult(int price);
    public static event WeaponPurchaseResult WeaponPurchase;

    public GameObject[] weapons;
    private List<int> purchasedWeapons = new List<int>();
    private int activeWeaponIndex;

    // Start is called before the first frame update
    void Start()
    {
        //THIS IS FOR DEBUG ONLY
        purchasedWeapons.Add(0);
        ResetActiveWeapons();
        Combat.PlayerDead += ResetActiveWeapons;
        ReturnToLobby.BackToLobby += ResetActiveWeapons;
        WeaponPrompt.ChangeWeapon += SwitchWeapon;
        WeaponPrompt.CheckOwnedWeapon += CheckIfPurchased;
        WeaponPrompt.PurchaseWeapon += PurchaseWeapon;
    }

    public void SwitchWeapon(int weaponID) {
        if (activeWeaponIndex != -1)
        {
            weapons[activeWeaponIndex].SetActive(false);
        }
        activeWeaponIndex = weaponID;
        weapons[activeWeaponIndex].SetActive(true);
    }

    private void ResetActiveWeapons() {
        for (int i = 0; i < weapons.Length; i++)
        {
            weapons[i].SetActive(false);
        }
        activeWeaponIndex = -1;
    }

    private void CheckIfPurchased(int weaponID) {
        bool value = false;
        if (purchasedWeapons.Contains(weaponID)) {
            value = true;
        }

        if (ResultForPurchaseCheck != null) {
            ResultForPurchaseCheck(value);
        }
    }

    private void PurchaseWeapon(int weaponID) {
        purchasedWeapons.Add(weaponID);
        if (WeaponPurchase != null) {
            WeaponPurchase(5);
        }
    }
}
