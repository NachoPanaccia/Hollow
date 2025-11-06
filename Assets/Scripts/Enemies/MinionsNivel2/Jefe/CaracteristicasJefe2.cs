using UnityEngine;

public class CaracteristicasJefe2 : MonoBehaviour, IDamageable
{
    [SerializeField] private ScriptableEnemies enemyData;
    public int currentHealth;

    private PooledObject pooled;

    void Awake()
    {
        pooled = GetComponent<PooledObject>();
    }

    void Start()
    {
        currentHealth = (enemyData != null) ? enemyData.maxHealth : 25;
    }

    public void TakeDamage(int damage)
    {
        currentHealth -= damage;
        if (currentHealth <= 0) Die();
    }

    void Die()
    {
        if (pooled != null) pooled.Release();
        else Destroy(gameObject);
    }
}

