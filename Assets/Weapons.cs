using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Weapons : MonoBehaviour
{

    public GameObject[] weapons;
    private int activeWeaponIndex;

    private void Awake()
    {
        for (int i = 0; i < weapons.Length; i++) { 
            weapons[i].SetActive(false);
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        activeWeaponIndex = -1;
        WeaponPrompt.ChangeWeapon += SwitchWeapon;
    }

    void Update()
    {
        
    }

    public void SwitchWeapon(int weaponID) {
        if (activeWeaponIndex != -1)
        {
            weapons[activeWeaponIndex].SetActive(false);
        }
        activeWeaponIndex = weaponID;
        weapons[activeWeaponIndex].SetActive(true);
    }
}
