using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossStats : MonoBehaviour
{
    public BossObject boss;

    private float areaDamage;
    private float rangeDamage;
    private float meleeDamage;
    private float bossHealth;
    private float meleeDelay;
    private float rangeDelay;
    private float areaDelay;

    private void Awake()
    {
        areaDamage = boss.areaDamage;
        rangeDamage = boss.rangeDamage;
        meleeDamage = boss.meleeDamage;
        bossHealth = boss.bossHealth;
        areaDelay = boss.areaDelay;
        rangeDelay = boss.rangeDelay;
        meleeDelay = boss.meleeDelay;
    }

    public float GetAreaDamage() {
        return areaDamage;
    }

    public float GetRangeDamage()
    {
        return rangeDamage;
    }
    public float GetMeleeDamage()
    {
        return meleeDamage;
    }

    public float GetBossHealth()
    {
        return bossHealth;
    }

    public float GetAreaDelay()
    {
        return areaDelay;
    }

    public float GetRangeDelay()
    {
        return rangeDelay;
    }

    public float GetMeleeDelay()
    {
        return meleeDelay;
    }

}
