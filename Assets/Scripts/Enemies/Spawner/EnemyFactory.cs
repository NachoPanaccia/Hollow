using UnityEngine;

public static class EnemyFactory
{
    public static GameObject CreateEnemy(GameObject enemyPrefab, Vector3 position, Quaternion rotation)
    {
        
        return PoolManager.Spawn(enemyPrefab, position, rotation);
    }
}
