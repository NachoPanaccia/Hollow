using UnityEngine;

public class ConcreteEnemyFactory : MonoBehaviour, IEnemyFactory
{
    [Header("Prefabs")]
    public GameObject bossPrefab;
    public GameObject minionPrefab;

    [Header("Prewarm")]
    public int bossPrewarm = 1;
    public int minionPrewarm = 6;

    private void Awake()
    {
        if (bossPrefab && bossPrewarm > 0) PoolManager.Prewarm(bossPrefab, bossPrewarm);
        if (minionPrefab && minionPrewarm > 0) PoolManager.Prewarm(minionPrefab, minionPrewarm);
    }

    public GameObject CreateBoss(Vector3 position, Quaternion rotation)
        => bossPrefab ? PoolManager.Spawn(bossPrefab, position, rotation) : null;

    public GameObject CreateMinion(Vector3 position, Quaternion rotation)
        => minionPrefab ? PoolManager.Spawn(minionPrefab, position, rotation) : null;
}

