using UnityEngine;

public class CaracteristicasEnemigo : MonoBehaviour, IDamageable
{
    [SerializeField] private ScriptableEnemies data;
    public int currentHealth;

    private PooledObject pooled;

    private void Awake()
    {
        pooled = GetComponent<PooledObject>();
    }

    public void Start()
    {
        currentHealth = (data != null) ? data.maxHealth : 5;
        LevelManager1.Instance.RegisterEnemy();
    }

    public void TakeDamage(int damage)
    {
        currentHealth -= damage;
        if (currentHealth <= 0) Die();
    }

    void Die()
    {
        LevelManager1.Instance.UnregisterEnemy();

        if (pooled != null) pooled.Release(); // [Materia: Pooling/GC minimization] 
        else Destroy(gameObject);
    }
}
