using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnersMinions : MonoBehaviour, IDamageable
{
    public GameObject minionPrefab;
    private int minionsCount = 0;
    private int maxMinions = 6;
    public int health = 100;

    void Start()
    {
        InvokeRepeating("SpawnMinion", 6f, 2f);
    }

    void SpawnMinion()
    {

        if (minionsCount >= maxMinions)
        {
            CancelInvoke("SpawnMinion");
        }

        if (minionPrefab != null)
        {
            Instantiate(minionPrefab, transform.position, Quaternion.identity);
            minionsCount++;
        }
    }
    public void TakeDamage(int damage)
    {
        health -= damage;
        Debug.Log($"Spawner recibió {damage} de daño. Salud actual: {health}");

        if (health <= 0)
        {
            DestroySpawner();
        }
    }
    private void DestroySpawner()
    {
        Debug.Log("El Spawner ha sido destruido.");
        Destroy(gameObject);
    }
}