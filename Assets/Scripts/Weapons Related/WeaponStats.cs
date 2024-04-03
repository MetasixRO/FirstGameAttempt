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
    private float weaponSpecialDamage;
    private float weaponSpecialCooldown;
    private SpecialAttack weaponSpecial;

    void Start()
    {
        weaponID = weapon.ID;
        weaponName = weapon.weaponName;
        weaponCooldown = weapon.cooldown;
        weaponDamage = weapon.damage;
        weaponSpecialDamage = weapon.specialDamage;
        weaponSpecialCooldown = weapon.specialCooldown;
        weaponSpecial = weapon.special;
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

    public SpecialAttack GetSpecialAttack() {
        return weaponSpecial;
    }

    public float GetWeaponSpecialDamage() {
        return weaponSpecialDamage;
    }

    public float GetWeaponSpecialCooldown() {
        return weaponSpecialCooldown;
    }
}
