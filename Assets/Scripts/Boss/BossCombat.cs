using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossCombat : EnemyCombat
{
    public override void EnemyStart()
    {
        BossStats stats = GetComponent<BossStats>();

        Debug.Log(stats.GetBossHealth());

        maxHealth = stats.GetBossHealth();

        base.currentHealth = maxHealth;
        if (healthBar != null)
        {
            healthBar.SetMaxHealth(currentHealth);
            healthBar.SetHealth(currentHealth);
        }
    }
}
