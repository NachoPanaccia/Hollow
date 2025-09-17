using UnityEngine;

public static class EnemyFactory
{
    public static GameObject CreateEnemy(GameObject enemyPrefab, Vector3 position, Quaternion rotation)
    {
        // [Materia: Object Pooling] crear desde pool, no instanciar siempre. 
        return PoolManager.Spawn(enemyPrefab, position, rotation);
    }
}
