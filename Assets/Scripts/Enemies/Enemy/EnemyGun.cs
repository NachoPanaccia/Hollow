using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyGun : MonoBehaviour
{
    private float fireRate = 0.5f;
    public GameObject bulletPrefab;
    public Transform firePoint;
    private string targetTag = "Player";
    private float nextFireTime = 4f;
    public AudioClip disparoEnemigoSound;
    public AudioSource audioSource;

    void Start()
    {
        audioSource = GetComponentInChildren<AudioSource>();
    }

    void Update()
    {
        ShootToPlayer();
    }

    void ShootToPlayer()
    {
        if (Time.time >= nextFireTime)
        {
            GameObject target = GameObject.FindGameObjectWithTag(targetTag);

            if (target != null)
            {
                Vector2 direction = target.transform.position - firePoint.position;
                direction.Normalize();
                float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
                Quaternion rotation = Quaternion.AngleAxis(angle, Vector3.forward);

                Vector2 bulletDirection = firePoint.up;

                Instantiate(bulletPrefab, firePoint.position, rotation).GetComponent<Rigidbody2D>().velocity = bulletDirection * 20f;

                nextFireTime = Time.time + Random.Range(fireRate * 0.5f, fireRate * 1.5f);

                if (disparoEnemigoSound != null && audioSource != null)
                {
                    audioSource.PlayOneShot(disparoEnemigoSound);
                }
            }
        }
    }
}