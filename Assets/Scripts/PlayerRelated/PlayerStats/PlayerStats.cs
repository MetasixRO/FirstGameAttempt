using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Player Stats", menuName = "Stats/Player Stats")]

public class PlayerStats : ScriptableObject
{
    public float originalMaxHealth;
    public float temporaryMaxHealth;
    public float currentMaxHealth;
}
