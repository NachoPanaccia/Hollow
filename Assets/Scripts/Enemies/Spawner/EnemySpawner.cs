using UnityEngine;
using System.Collections;

public class EnemySpawner : MonoBehaviour
{
    [Header("Config")]
    public GameObject enemyPrefab;
    public float spawnInterval = 10f;
    public int prewarm = 4;
    public int maxAlive = 12;                 // [Materia: Frame Budget] limitar carga. 
    public float minPlayerDistance = 6f;

    [Header("Spawn Offset")]
    private float spawnHeight = 1.2f;

    private Transform player;

    private void Start()
    {
        if (enemyPrefab != null && prewarm > 0) PoolManager.Prewarm(enemyPrefab, prewarm);
        var p = GameObject.FindGameObjectWithTag("Player");
        if (p) player = p.transform;
        StartCoroutine(Loop());
    }

    private IEnumerator Loop()
    {
        var wait = new WaitForSeconds(spawnInterval);
        while (true)
        {
            TrySpawn();
            yield return wait;
        }
    }

    private void TrySpawn()
    {
        if (enemyPrefab == null) return;

        // cap suave por intervalo (no buscamos contar en cada frame)
        // Si tenés LevelManager1 con contador, lo ideal es consultarlo. Aquí uso tag para no depender.
        var current = GameObject.FindGameObjectsWithTag("Enemy").Length;
        if (current >= maxAlive) return;

        if (player != null)
        {
            float dist = Vector3.Distance(player.position, transform.position);
            if (dist < minPlayerDistance) return;
        }

        Vector3 pos = transform.position + Vector3.up * spawnHeight;
        EnemyFactory.CreateEnemy(enemyPrefab, pos, Quaternion.identity);
    }
}
