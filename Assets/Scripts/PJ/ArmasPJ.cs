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
        if (audioSource == null) audioSource = GetComponent<AudioSource>();
    }

    void Update()
    {
        Shoot();
    }

    private void Shoot()
    {
       
        if (Input.GetMouseButtonDown(0))
        {
            if (bulletPool == null || cannonPosition == null) return;

           
            Vector3 bulletPos = cannonPosition.position;

           
            Vector3 mouseWorld = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            mouseWorld.z = bulletPos.z;
            Vector3 dir = (mouseWorld - bulletPos).normalized;

            
            GameObject bulletGO = bulletPool.GetObject();
            bulletGO.transform.SetPositionAndRotation(bulletPos, Quaternion.identity);

           
            gunBullet bullet = bulletGO.GetComponent<gunBullet>();
            if (bullet != null)
            {
                bullet.bulletPool = this.bulletPool; 
                bullet.SetDirection(dir);
            }

           
            if (disparoSound != null && audioSource != null)
                audioSource.PlayOneShot(disparoSound);
        }
    }
}
