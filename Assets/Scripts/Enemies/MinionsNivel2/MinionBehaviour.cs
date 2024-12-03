using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MinionBehaviour : MonoBehaviour, IDamageable
{
    [SerializeField] private ScriptableEnemies enemyData;
    //private int minionLifes = 4;
    public int currentHealth;

    public GameObject CorazonVida;

    public void Start()
    {
        currentHealth = enemyData.maxHealth;

        LevelManager2.Instance.RegisterMinion();
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

    public void OnDestroy()
    {
        float dropChance = Random.value;

        if (dropChance <= 0.25f)
        {
            Instantiate(CorazonVida, transform.position, Quaternion.identity);
        }
    }

    void Die()
    {
        LevelManager2.Instance.UnregisterMinion();
        Destroy(gameObject);
    }
}