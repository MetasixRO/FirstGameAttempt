using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    private static int generalSpawnerID = 1;
    [SerializeField] int instanceID;

    public delegate void EnemySpawnerEvent(int counter);
    public static event EnemySpawnerEvent EnemyCounter;

    public List<GameObject> enemies = new List<GameObject>();
    [SerializeField] private float delayBetweenSpawns = 2f;

    private void Start()
    {
        instanceID = generalSpawnerID++;
        LevelManager.EnterNextArena += ActivateSpawnerBasedOnID;
    }

    public void ActivateSpawner() {
        int numberOfEnemies = Random.Range(2, 5);
        if (EnemyCounter != null) {
            EnemyCounter(numberOfEnemies);
        }
        StartCoroutine(SpawnEnemies(numberOfEnemies,delayBetweenSpawns));
    }

    private void ActivateSpawnerBasedOnID(int id) {
        if (instanceID == id) {
            int numberOfEnemies = Random.Range(2, 5);
            if (EnemyCounter != null)
            {
                EnemyCounter(numberOfEnemies);
            }
            StartCoroutine(SpawnEnemies(numberOfEnemies, delayBetweenSpawns));
        }
    }


    private IEnumerator SpawnEnemies(int numberOfEnemies, float delayBetweenSpawns) {
        for (int i = 0; i < numberOfEnemies; i++) {
            int enemyIndex = Random.Range(0, enemies.Count);
            GameObject enemy = enemies[enemyIndex];

            Vector3 spawnOffset = new Vector3(Random.Range(-2f, 2f), 0f, Random.Range(-2f, 2f));
            Vector3 spawnPosition = transform.position + spawnOffset;
            Instantiate(enemy, spawnPosition, Quaternion.identity);
            enemy.GetComponent<Controller>().ManageAgent();

            yield return new WaitForSeconds(delayBetweenSpawns);
        }
    }
}
