using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MinionBehaviour : MonoBehaviour, IDamageable
{
    [SerializeField] private ScriptableEnemies enemyData;
    [SerializeField] private GameObject CorazonVida;
    public int currentHealth;

    private PooledObject pooled;

    public void Start()
    {
        currentHealth = (enemyData != null) ? enemyData.maxHealth : 4;
        pooled = GetComponent<PooledObject>();
        LevelManager2.Instance.RegisterMinion();
    }

    public void TakeDamage(int damage)
    {
        currentHealth -= damage;
        if (currentHealth <= 0) Die();
    }

    void Die()
    {
        
        if (CorazonVida && Random.value <= 0.25f)
            Instantiate(CorazonVida, transform.position, Quaternion.identity);

        LevelManager2.Instance.UnregisterMinion();

        if (pooled != null) pooled.Release(); // [Materia: Pooling/GC] 
        else Destroy(gameObject);
    }
}
