using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Weapon", menuName = "Weapon")]
public class Weapon : ScriptableObject
{
    public string weaponName;
    public int ID;
    public float cooldown;
    public float damage;
    public float specialDamage;
    public float specialCooldown;
    public SpecialAttack special;
}
