using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ArmasPJ : MonoBehaviour
{
    public GameObject bulletPrefab;
    public Transform cannonPosition;
    public AudioClip disparoSound;
    public AudioSource audioSource;
    public Pool bulletPool;

    void Start()
    {
        audioSource = GetComponent<AudioSource>();
    }

    void Update()
    {
        Shoot();
    }

    private void Shoot()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            Vector3 bulletPosition = cannonPosition.position;
            GameObject gunBullets = bulletPool.GetObject();
            gunBullets.transform.SetPositionAndRotation(bulletPosition, transform.rotation);
            gunBullet gunBulletsComponent = gunBullets.GetComponent<gunBullet>();
            gunBulletsComponent.SetDirection(transform.up);
            gunBulletsComponent.bulletPool = this.bulletPool;

            if (disparoSound != null && audioSource != null)
            {
                audioSource.PlayOneShot(disparoSound);
            }
        }
    }
}