using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InstaKillReceiver : MonoBehaviour
{
    private int enemyKillCounter;

    void Start()
    {
        enemyKillCounter = 0;
        LuckNPCAbility.SendKillSignal += SetCounter;
        InstaKillHelper.SignupThisEnemy += KillEnemy;
    }

    private void SetCounter(int counter) {
        enemyKillCounter = counter;
    }

    private void KillEnemy(EnemyCombat combatComponent) {
        if (enemyKillCounter > 0) {
            combatComponent.TakeDamage(999);
            enemyKillCounter--;
        }
    }
    
}
