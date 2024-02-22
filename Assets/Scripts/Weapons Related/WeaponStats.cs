using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponStats : MonoBehaviour
{

    public Weapon weapon;

    private string weaponName;
    private int weaponID;
    private float weaponCooldown;
    private float weaponDamage;

    void Start()
    {
        weaponID = weapon.ID;
        weaponName = weapon.weaponName;
        weaponCooldown = weapon.cooldown;
        weaponDamage = weapon.damage;
    }

    public int GetWeaponID() { 
        return weaponID;
    }

    public float GetWeaponCooldown() {
        return weaponCooldown;
    }

    public float GetWeaponDamage() {
        return weaponDamage;
    }

    public string GetWeaponName() {
        return weaponName;
    }
}
