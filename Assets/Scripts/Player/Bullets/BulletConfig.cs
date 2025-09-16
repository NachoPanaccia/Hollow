using UnityEngine;

[CreateAssetMenu(fileName = "BulletConfig", menuName = "Game/Weapons/Bullet Config")]
public class BulletConfig : ScriptableObject
{
    [Header("Prefab y Daño")]
    public GameObject bulletPrefab; // Debe tener BulletController
    public int damage = 5;


    [Header("Movimiento")]
    [Min(0f)] public float speed = 30f;


    [Header("Ciclo de Vida")]
    [Min(0f)] public float lifeTime = 3f;


    [Header("Audio Opcional")]
    public AudioClip fireSfx;
}