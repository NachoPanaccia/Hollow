using UnityEngine;

public class ArmasPJ : MonoBehaviour
{
    [Header("Referencias")]
    [SerializeField] private Transform firePoint;
    [SerializeField] private GameObject bulletPrefab;
    [SerializeField] private Pool bulletPool;

    [Header("Disparo")]
    [SerializeField] private bool semiAuto = true;
    [SerializeField] private float fireRate = 8f;
    [SerializeField] private float bulletSpeed = 12f;
    [SerializeField] private float bulletLifetime = 5f;
    [SerializeField] private AudioSource audioSource;
    [SerializeField] private AudioClip bulletSfx;
    [Range(0f, 1f)][SerializeField] private float bulletSfxVolume = 0.9f;

    [Header("Dirección")] // NO SE ESTA USANDO.
    [SerializeField] private bool usarRightComoForward = true;

    private float _nextFireAtUnscaled = 0f;

    private void Update()
    {
        if (GameManager.InputLocked) return;

        if (semiAuto)
        {
            if (Input.GetMouseButtonDown(0))
            {
                Disparar();
            }
        }
        else
        {
            if (Input.GetMouseButton(0) && Time.unscaledTime >= _nextFireAtUnscaled)
            {
                Disparar();
                _nextFireAtUnscaled = Time.unscaledTime + (1f / Mathf.Max(0.01f, fireRate));
            }
        }
    }

    private void Disparar()
    {
        if (!firePoint || !bulletPrefab) return;

        GameObject bala;

        // 1) Sacar la bala del pool si existe
        if (bulletPool != null)
        {
            bala = bulletPool.GetObject();
            bala.transform.SetPositionAndRotation(firePoint.position, firePoint.rotation);
        }
        else
        {
            // Fallback: si no hay pool asignado, seguimos como antes
            bala = Instantiate(bulletPrefab, firePoint.position, firePoint.rotation);
        }

        // 2) Forzar que NO aplique físicas
        Rigidbody2D rb = bala.GetComponent<Rigidbody2D>();
        if (rb)
        {
            rb.velocity = Vector2.zero;
            rb.angularVelocity = 0f;
            rb.gravityScale = 0f;
            rb.bodyType = RigidbodyType2D.Kinematic;
            rb.constraints = RigidbodyConstraints2D.FreezeRotation;
        }

        // 3) Pasar dirección y pool a la bala
        var gb = bala.GetComponent<gunBullet>();
        if (gb != null)
        {
            gb.bulletPool = bulletPool;  // ← MUY IMPORTANTE
            Vector3 dir = usarRightComoForward ? (Vector3)firePoint.right : (Vector3)firePoint.up;
            gb.SetDirection(dir);
        }

        // 4) Fallback de vida solo si NO es gunBullet
        if (gb == null && bulletLifetime > 0f)
        {
            Destroy(bala, bulletLifetime);
        }

        // 5) SFX de disparo (igual que antes)
        if (bulletSfx && audioSource)
        {
            audioSource.PlayOneShot(bulletSfx, bulletSfxVolume);
        }
        else if (bulletSfx)
        {
            AudioSource.PlayClipAtPoint(bulletSfx, firePoint ? firePoint.position : transform.position, bulletSfxVolume);
        }
    }
}
