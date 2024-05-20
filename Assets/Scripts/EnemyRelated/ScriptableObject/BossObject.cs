using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Boss", menuName = "Boss")]
public class BossObject : ScriptableObject
{
    public float areaDamage;
    public float rangeDamage;
    public float meleeDamage;
    public float bossHealth;
    public float areaDelay;
    public float rangeDelay;
    public float meleeDelay;
}
