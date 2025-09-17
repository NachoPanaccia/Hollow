using UnityEngine;

public class EnemyGun : MonoBehaviour
{
    [SerializeField] private ScriptableEnemies data;
    [SerializeField] private Transform firePoint;
    [SerializeField] private AudioSource audioSource;

    private Transform target;             
    private float nextShootTime;

    private void Awake()
    {
        if (audioSource == null) audioSource = GetComponentInChildren<AudioSource>();
    }

    private void Start()
    {
        var player = GameObject.FindGameObjectWithTag("Player"); 
        if (player != null) target = player.transform;

        nextShootTime = Time.time + 0.25f; 
    }

    private void Update()
    {
        if (target == null || data == null || firePoint == null) return;

        if (Time.time >= nextShootTime)
        {
            
            Vector2 dir = (target.position - firePoint.position);
            float angle = Mathf.Atan2(dir.y, dir.x) * Mathf.Rad2Deg;
            firePoint.rotation = Quaternion.AngleAxis(angle, Vector3.forward);

           
            var prefab = data.bulletPrefab;
            if (prefab == null) return;

            var go = PoolManager.Spawn(prefab, firePoint.position, firePoint.rotation);
            var rb = go.GetComponent<Rigidbody2D>();
            if (rb != null)
            {
                rb.velocity = (Vector2)firePoint.right * data.bulletSpeed; 
            }

            if (data.shootSfx != null && audioSource != null)
                audioSource.PlayOneShot(data.shootSfx);

            nextShootTime = Time.time + (1f / Mathf.Max(0.01f, data.fireRate));

            
        }
    }
}
