using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Weapons : MonoBehaviour
{

    public GameObject[] weapons;
    private int activeWeaponIndex;

    // Start is called before the first frame update
    void Start()
    {
        ResetActiveWeapons();
        Combat.PlayerDead += ResetActiveWeapons;
        ReturnToLobby.BackToLobby += ResetActiveWeapons;
        WeaponPrompt.ChangeWeapon += SwitchWeapon;
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
}
