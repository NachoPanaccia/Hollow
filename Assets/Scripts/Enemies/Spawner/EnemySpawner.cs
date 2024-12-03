using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    public GameObject enemyPrefab;
    private float spawnHeight = 1.2f;

    void Start()
    {
        InvokeRepeating("SpawnEnemy", 0f, 10f);
    }

    void SpawnEnemy()
    {
        Vector3 spawnPosition = transform.position + Vector3.up * spawnHeight;

        
        EnemyFactory.CreateEnemy(enemyPrefab, spawnPosition, Quaternion.identity);
    }
}