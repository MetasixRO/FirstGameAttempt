using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemiesClearedEvent : MonoBehaviour
{
    public delegate void AllEnemiesDied();
    public static event AllEnemiesDied SpawnAbilityBoon;
    public static event AllEnemiesDied SpawnCoins;
    public static event AllEnemiesDied SpawnKeys;
    public static event AllEnemiesDied SpawnHeal;

    private int aliveEnemiesCounter;

    private void Start()
    {
        aliveEnemiesCounter = 0;
        EnemyCombat.EnemyDead += CheckAllDead;
        EnemySpawner.EnemyCounter += SetTotalEnemies;
        newDeadState.RespawnPlayer += ClearTotalEnemies;
    }

    private void CheckAllDead()
    {
        aliveEnemiesCounter--;
        if (aliveEnemiesCounter == 0)
        {
            ChoseReward();
        }
    }

    private void SetTotalEnemies(int counter)
    {
        aliveEnemiesCounter += counter;
    }

    private void ClearTotalEnemies() {
        aliveEnemiesCounter = 0;
    }

    private void ChoseReward() {
        int rewardNumber = Random.Range(1, 5);
        switch (rewardNumber) {
            case 1: 
                if (SpawnAbilityBoon != null) {
                    SpawnAbilityBoon();
                }
                break;
            case 2:
                if (SpawnCoins != null) {
                    SpawnCoins();
                }
                break;
            case 3:
                if (SpawnKeys != null)
                {
                    SpawnKeys();
                }
                break;
            case 4:
                if (SpawnHeal != null)
                {
                    SpawnHeal();
                }
                break;

        }
    }
}
