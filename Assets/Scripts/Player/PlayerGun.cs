using UnityEngine;


[RequireComponent(typeof(AudioSource))]
public class PlayerGun : MonoBehaviour
{
    [Header("Referencias")]
    [SerializeField] private BulletConfig bulletConfig; // Para SFX
    [SerializeField] private Transform muzzle;
    [SerializeField] private BulletPool bulletPool;


    [Header("Entrada")]
    [SerializeField] private KeyCode fireKey = KeyCode.Space;
    [SerializeField] private bool useMouseDirection = false; // Si querés apuntar al mouse


    private AudioSource audioSource;


    private void Awake()
    {
        audioSource = GetComponent<AudioSource>();
    }


    private void Update()
    {
        if (Input.GetKeyDown(fireKey))
        {
            Fire();
        }
    }


    private void Fire()
    {
        if (bulletPool == null)
        {
            Debug.LogWarning("PlayerGun: bulletPool no seteado.");
            return;
        }


        if (!bulletPool.TryGet(out GameObject bulletGO))
        {
            return;
        }


        // Posición y rotación
        bulletGO.transform.SetPositionAndRotation(muzzle.position, transform.rotation);


        // Dirección
        Vector3 dir = transform.up; // por defecto, como tu versión actual
        if (useMouseDirection)
        {
            Vector3 mouseWorld = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            mouseWorld.z = muzzle.position.z;
            dir = (mouseWorld - muzzle.position).normalized;
            bulletGO.transform.up = dir; // orientar el sprite si querés
        }


        // Configurar la bala
        var ctrl = bulletGO.GetComponent<IBullet>();
        ctrl.SetDirection(dir);


        // SFX
        if (bulletConfig != null && bulletConfig.fireSfx != null)
        {
            audioSource.PlayOneShot(bulletConfig.fireSfx);
        }
    }
}