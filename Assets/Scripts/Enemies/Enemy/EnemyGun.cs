using UnityEngine;

public class EnemyGun : MonoBehaviour
{
    [SerializeField] private ScriptableEnemies data;
    [SerializeField] private Transform firePoint;
    [SerializeField] private AudioSource audioSource;

    private Transform target;                 // [Materia: Caching] cache de referencia. 
    private float nextShootTime;

    private void Awake()
    {
        if (audioSource == null) audioSource = GetComponentInChildren<AudioSource>();
    }

    private void Start()
    {
        var player = GameObject.FindGameObjectWithTag("Player"); // se resuelve una vez
        if (player != null) target = player.transform;

        nextShootTime = Time.time + 0.25f; // arranque suave
    }

    private void Update()
    {
        if (target == null || data == null || firePoint == null) return;

        if (Time.time >= nextShootTime)
        {
            // Rotar firePoint hacia el objetivo
            Vector2 dir = (target.position - firePoint.position);
            float angle = Mathf.Atan2(dir.y, dir.x) * Mathf.Rad2Deg;
            firePoint.rotation = Quaternion.AngleAxis(angle, Vector3.forward);

            // Disparo usando la MISMA dirección que la rotación
            var prefab = data.bulletPrefab;
            if (prefab == null) return;

            var go = PoolManager.Spawn(prefab, firePoint.position, firePoint.rotation);
            var rb = go.GetComponent<Rigidbody2D>();
            if (rb != null)
            {
                rb.velocity = (Vector2)firePoint.right * data.bulletSpeed; // right = eje local X del firePoint
            }

            if (data.shootSfx != null && audioSource != null)
                audioSource.PlayOneShot(data.shootSfx);

            nextShootTime = Time.time + (1f / Mathf.Max(0.01f, data.fireRate));

            // [Materia: Expected Path] evitar Find cada tiro y alinear rotación/dirección. 
        }
    }
}
