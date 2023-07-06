using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    public GameObject[] enemyPrefabs;
    public float[] spawnChances;
    public int currentSpawnCount = 0;
    public int maxSpawnCount = 10;
    public float startSpawnDelay = 2f;
    public float spawnRateMultiplier = 0.9f;

    public float spawnRate;

    private void Start()
    {
        InvokeRepeating("SpawnEnemy", startSpawnDelay, spawnRate);
    }

    private void SpawnEnemy()
    {
        if (currentSpawnCount >= maxSpawnCount)
        {
            CancelInvoke("SpawnEnemy");
            return;
        }

        float totalChances = 0f;


        for (int i = 0; i < spawnChances.Length; i++)
        {
            totalChances += spawnChances[i];
        }


        float randomValue = Random.Range(0f, totalChances);

        float chanceSum = 0f;
        GameObject enemyPrefab = null;


        for (int i = 0; i < spawnChances.Length; i++)
        {
            chanceSum += spawnChances[i];

            if (randomValue <= chanceSum)
            {
                enemyPrefab = enemyPrefabs[i];
                break;
            }
        }


        if (enemyPrefab != null)
        {
            Instantiate(enemyPrefab, transform.position, Quaternion.identity);
        }

        currentSpawnCount++;
        spawnRate *= spawnRateMultiplier;
    }
}
