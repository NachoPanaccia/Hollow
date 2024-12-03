using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class EnemyFactory
{
    public static GameObject CreateEnemy(GameObject enemyPrefab, Vector3 position, Quaternion rotation)
    {
        
        GameObject enemy = GameObject.Instantiate(enemyPrefab, position, rotation);

       
        return enemy;
    }
}