using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Jefe3Behaviour : MonoBehaviour, IDamageable
{
    [SerializeField] private ScriptableEnemies enemyData;
    //private int JefeLifes = 15;
    public int currentHealth;

    void Start()
    {
        currentHealth = enemyData.maxHealth;
    }

    public void TakeDamage(int damage)
    {
        currentHealth -= damage;
        Debug.Log($"Enemigo recibi� {damage} de da�o. Vida actual: {currentHealth}");

        if (currentHealth <= 0)
        {
            Die();
        }
    }

    void Die()
    {
        LevelManager3.Instance.BossDefeated();

        Destroy(gameObject);
    }
}