using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossCombat : EnemyCombat
{
    public delegate void CombatEvent(float damage);
    public static event CombatEvent MeleeDamage;
    public static event CombatEvent AreaDamage;
    public static event CombatEvent RangeDamage;
    public override void EnemyStart()
    {
        Debug.Log("Calling Start");
        BossStats stats = GetComponent<BossStats>();

        Debug.Log(stats.GetBossHealth());

        maxHealth = stats.GetBossHealth();

        base.currentHealth = maxHealth;
        if (healthBar != null)
        {
            healthBar.SetMaxHealth(currentHealth);
            healthBar.SetHealth(currentHealth);
        }

        GetComponent<BossAnimations>().SetDelay(stats.GetMeleeDelay(), stats.GetAreaDelay(), stats.GetRangeDelay());
        MeleeDamage?.Invoke(stats.GetMeleeDamage());
        AreaDamage?.Invoke(stats.GetAreaDamage());
        RangeDamage?.Invoke(stats.GetRangeDamage());
    }
}
