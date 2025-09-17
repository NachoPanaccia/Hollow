using UnityEngine;

[CreateAssetMenu(fileName = "EnemySO", menuName = "Scriptable/Enemy")]
public class ScriptableEnemies : ScriptableObject
{
    [Header("Vida y movimiento")]
    public int maxHealth = 5;
    public float speed = 3f;

    [Header("Ataque (si aplica)")]
    public int damage = 1;
    public float fireRate = 0.5f;       // tiros/seg
    public float bulletSpeed = 20f;
    public float detectRange = 12f;
    public float attackRange = 8f;

    [Header("Evasión")]
    public float avoidanceRadius = 3f;
    public float avoidanceWeight = 1f;

    [Header("Prefabs/vfx/sfx opcionales")]
    public GameObject bulletPrefab;
    public AudioClip shootSfx;
}
