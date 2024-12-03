using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CaracteristicasJefe2 : MonoBehaviour, IDamageable
{
    [SerializeField] private ScriptableEnemies enemyData;
    //private int jefe2Lifes = 25;
    public int currentHealth;

    void Start()
    {
        currentHealth = enemyData.maxHealth;
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
        Destroy(gameObject);
    }
}
