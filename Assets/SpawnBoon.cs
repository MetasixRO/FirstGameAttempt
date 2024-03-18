using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnBoon : MonoBehaviour
{
    public delegate void AllEnemiesDied();
    public static event AllEnemiesDied SpawnAbilityBoon;

    //FOR DEBUG FOR NOW, WILL BE UPDATED DEPDENDING ON THE NUMBER OF ENEMIES
    private int aliveEnemiesCounter;

    private void Start()
    {
        //FOR DEBBUG FOR NOW
        aliveEnemiesCounter = 1;
        EnemyCombat.EnemyDead += CheckAllDead;
    }

    private void CheckAllDead() {
        aliveEnemiesCounter--;
        if (aliveEnemiesCounter == 0 && SpawnAbilityBoon != null) {
            SpawnAbilityBoon();
        }
    }
}
