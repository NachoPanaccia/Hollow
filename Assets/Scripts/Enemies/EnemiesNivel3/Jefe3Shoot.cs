using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Jefe3Shoot : MonoBehaviour
{
    public GameObject bulletJefe3Prefab;
    public float[] fireRates;
    public float nextFireTime = 5f;
    public float bulletSpeed = 0.25f;

    private Transform[] firePoints;

    private void Start()
    {
        firePoints = new Transform[4];
        for (int i = 0; i < 4; i++)
        {
            firePoints[i] = transform.Find("FirePoint" + (i + 1));
        }
    }

    private void Update()
    {
        nextFireTime -= Time.deltaTime;

        if (nextFireTime <= 0)
        {
            FireFromAllPoints();
            nextFireTime = CalculateNextFireTime();
        }
    }

    private void FireFromAllPoints()
    {
        for (int i = 0; i < firePoints.Length; i++)
        {
            if (firePoints[i] != null)
            {
              
                GameObject bullet = Instantiate(bulletJefe3Prefab, firePoints[i].position, firePoints[i].rotation);

                
                Rigidbody2D bulletRigidbody = bullet.GetComponent<Rigidbody2D>();
                Vector2 direction = firePoints[i].up; 
                bulletRigidbody.AddForce(direction * bulletSpeed, ForceMode2D.Impulse);
            }
        }
    }

    private float CalculateNextFireTime()
    {
        int randomIndex = Random.Range(0, fireRates.Length);
        return fireRates[randomIndex];
    }
}
//public class Jefe3Shoot : MonoBehaviour
//{
//    public GameObject bulletJefe3Prefab; 
//    public float[] fireRates; 
//    public float nextFireTime = 5f; 
//    public float bulletSpeed = 0.25f; 

//    private Transform[] firePoints; 

//    private void Start()
//    {
//        firePoints = new Transform[4];
//        for (int i = 0; i < 4; i++)
//        {
//            firePoints[i] = transform.Find("FirePoint" + (i + 1));
//        }
//    }

//    private void Update()
//    {
//        nextFireTime -= Time.deltaTime;

//        if (nextFireTime <= 0)
//        {
//            for (int i = 0; i < 4; i++)
//            {
//                Fire(i);
//            }

//            nextFireTime = CalculateNextFireTime();
//        }
//    }

//    private void Fire(int firePointIndex)
//    {
//        GameObject bullet = Instantiate(bulletJefe3Prefab, firePoints[firePointIndex].position, Quaternion.identity);

//        Rigidbody2D bulletRigidbody = bullet.GetComponent<Rigidbody2D>();

//        Vector2 direction = firePoints[firePointIndex].up;

//        bulletRigidbody.AddForce(direction * bulletSpeed, ForceMode2D.Impulse);
//    }

//    private float CalculateNextFireTime()
//    {
//        int randomIndex = Random.Range(0, fireRates.Length);
//        return fireRates[randomIndex];
//    }
//}