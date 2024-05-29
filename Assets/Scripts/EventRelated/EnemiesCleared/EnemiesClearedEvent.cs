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
    public static event AllEnemiesDied SpawnAmbrosia;
    public static event AllEnemiesDied EnemiesCleared;

    private bool chooseRandomly;
    private int rewardNumber;
    private int aliveEnemiesCounter;

    private void Start()
    {
        rewardNumber = 0;
        chooseRandomly = true;
        aliveEnemiesCounter = 0;
        EnemyCombat.EnemyDead += CheckAllDead;
        EnemySpawner.EnemyCounter += SetTotalEnemies;
        newDeadState.RespawnPlayer += ClearTotalEnemies;

        LevelManager.SelectNextReward += SetNextReward;
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
        //Debug.Log(counter);
        aliveEnemiesCounter += counter;
    }

    private void ClearTotalEnemies() {
        aliveEnemiesCounter = 0;
        chooseRandomly = true;
    }

    private void ChoseReward() {
        if (chooseRandomly)
        {
            rewardNumber = Random.Range(0, 5);
        }
        switch (rewardNumber) {
            case 0: 
                if (SpawnAbilityBoon != null) {
                    SpawnAbilityBoon();
                    EnemiesCleared();
                }
                break;
            case 1:
                if (SpawnCoins != null) {
                    SpawnCoins();
                    EnemiesCleared();
                }
                break;
            case 2:
                if (SpawnKeys != null)
                {
                    SpawnKeys();
                    EnemiesCleared();
                }
                break;
            
            case 3:
                if (SpawnHeal != null)
                {
                    SpawnHeal();
                    EnemiesCleared();
                }
                break;
            case 4:
                if (SpawnAmbrosia != null)
                {
                    SpawnAmbrosia();
                    EnemiesCleared();
                }
                break;

        }
    }

    public void SetNextReward(int number) {
        rewardNumber = number;
        Debug.Log("Finally the next reward will be : " + rewardNumber);
        chooseRandomly = false;
    }
}
