using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyDestroyer : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        EnemyCombat.EnemyToDelete += DeleteEnemy;
    }

    private void DeleteEnemy(GameObject enemy) {
        if (enemy != null)
        {
            Destroy(enemy);
        }
    }
}
