using UnityEngine;

[CreateAssetMenu(fileName = "CharacterStatsConfig", menuName = "Game/Player/Character Stats Config")]
public class CharacterStatsConfig : ScriptableObject
{
    [Header("Vida")]
    [Min(1)] public int maxHealth = 100;
    [Min(0)] public float invulnerabilitySeconds = 0.2f;

    [Header("Curaciones (opcional)")]
    public int healOnPickup = 10;
    public bool clampHealToMax = true;
}