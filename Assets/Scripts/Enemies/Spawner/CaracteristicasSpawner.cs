using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CaracteristicasSpawner : MonoBehaviour, IDamageable

{
    private int enemySpawnerLifes = 10;
    public int currentHealth;

    void Start()
    {
        currentHealth = enemySpawnerLifes;

        LevelManager1.Instance.RegisterSpawner();
    }

    public void TakeDamage(int damageAmount)
    {
        currentHealth -= damageAmount;

        if (currentHealth <= 0)
        {
            Die();
        }
    }

    public void Die()
    {
        LevelManager1.Instance.UnregisterSpawner();

        Destroy(gameObject);
    }
}