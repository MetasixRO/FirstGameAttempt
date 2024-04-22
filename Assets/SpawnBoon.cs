using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnBoon : MonoBehaviour
{
    public delegate void AllEnemiesDied();
    public static event AllEnemiesDied SpawnAbilityBoon;
    private int aliveEnemiesCounter;

    private void Start()
    {
        //FOR DEBBUG FOR NOW
        aliveEnemiesCounter = 0;
        EnemyCombat.EnemyDead += CheckAllDead;
        EnemySpawner.EnemyCounter += SetTotalEnemies;
    }

    private void CheckAllDead() {
        aliveEnemiesCounter--;
        if (aliveEnemiesCounter == 0 && SpawnAbilityBoon != null) {
            SpawnAbilityBoon();
        }
    }

    private void SetTotalEnemies(int counter) {
        aliveEnemiesCounter += counter;
    }
}
