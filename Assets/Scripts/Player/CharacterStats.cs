using UnityEngine;
using UnityEngine.Events;

public class CharacterStats : MonoBehaviour, IDamageable, IStatsProvider
{
    [SerializeField] private CharacterStatsConfig config;

    [Header("Eventos")]
    public UnityEvent<int, int> OnHealthChanged; // (current, max)
    public UnityEvent<int> OnDamaged;             // damageAmount
    public UnityEvent<int> OnHealed;              // healAmount
    public UnityEvent OnDied;

    private int currentHealth;
    private float invulnTimer;

    public int CurrentHealth => currentHealth;
    public int MaxHealth => config != null ? config.maxHealth : 100;

    private void Awake()
    {
        if (config == null) Debug.LogWarning("CharacterStats: falta CharacterStatsConfig");
        currentHealth = MaxHealth;
        OnHealthChanged?.Invoke(currentHealth, MaxHealth);
    }

    private void Update()
    {
        if (invulnTimer > 0f) invulnTimer -= Time.deltaTime;
    }

    // IDamageable
    public void TakeDamage(int amount)
    {
        if (amount <= 0) return;
        if (invulnTimer > 0f) return; // invulnerable

        currentHealth = Mathf.Max(0, currentHealth - amount);
        OnDamaged?.Invoke(amount);
        OnHealthChanged?.Invoke(currentHealth, MaxHealth);

        invulnTimer = (config != null) ? config.invulnerabilitySeconds : 0.2f;

        if (currentHealth <= 0)
        {
            OnDied?.Invoke();
            // Dejá que otro sistema decida qué hacer al morir (respawn, menú, etc.)
        }
    }

    // IStatsProvider
    public void Heal(int amount)
    {
        if (amount <= 0) return;
        int prev = currentHealth;
        currentHealth += amount;
        if (config == null || config.clampHealToMax) currentHealth = Mathf.Min(currentHealth, MaxHealth);

        int healed = currentHealth - prev;
        if (healed > 0)
        {
            OnHealed?.Invoke(healed);
            OnHealthChanged?.Invoke(currentHealth, MaxHealth);
        }
    }
}
