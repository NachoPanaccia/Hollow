using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CaracteristicasEnemigo : MonoBehaviour, IDamageable
{
    [SerializeField] private ScriptableEnemies enemyData;
    //private int enemyLifes = 10;
    public int currentHealth;
    public GameObject enemyPrefab, IDamageable;


    public void Start()
    {
        currentHealth = enemyData.maxHealth;

        LevelManager1.Instance.RegisterEnemy();
    }

    public void TakeDamage(int damage)
    {
        currentHealth -= damage;
        Debug.Log($"Enemigo recibió {damage} de daño. Vida actual: {currentHealth}");

        if (currentHealth <= 0)
        {
            Die();
        }
    }

    void Die()
    {
        LevelManager1.Instance.UnregisterEnemy();

        Destroy(gameObject);

    }
}
