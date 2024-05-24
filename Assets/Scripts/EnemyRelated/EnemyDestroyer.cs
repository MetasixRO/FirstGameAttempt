using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyDestroyer : MonoBehaviour
{
    private void Start()
    {
        EnemyCombat.EnemyForCleaning += DeleteEnemy;
    }

    private void DeleteEnemy(GameObject enemy) {
        if (enemy != null) {
            Destroy(enemy);
        }
    }
}
