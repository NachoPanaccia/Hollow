using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class BossMinionFactory : MonoBehaviour
{
    
    //[SerializeField] private GameObject bossPrefab;
    [SerializeField] private GameObject minionPrefab;

    //private float bossSpawnHeight = 1.5f;
    private float minionSpawnHeight = 1.0f;

    //private bool bossSpawned = false;
    private int minionCount = 0;
    private int maxMinions = 6;

    void Start()
    {
        SpawnBossAndMinions();
    }

    void SpawnBossAndMinions()
    {
        
        //if (!bossSpawned && bossPrefab != null)
        //{
        //    Vector3 bossPosition = transform.position + Vector3.up * bossSpawnHeight;
        //    Instantiate(bossPrefab, bossPosition, Quaternion.identity); 
        //    bossSpawned = true;
        //}

        
        while (minionCount < maxMinions && minionPrefab != null)
        {
            Vector3 minionPosition = transform.position + new Vector3(Random.Range(-2f, 2f), Random.Range(-2f, 2f), 0) * minionSpawnHeight;
            Instantiate(minionPrefab, minionPosition, Quaternion.identity); 
            minionCount++; 
        }
    }
}