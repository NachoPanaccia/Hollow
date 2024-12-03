using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ActivateSpawners : MonoBehaviour
{
    [SerializeField] public GameObject[] spawners;

    private void ActivateEnemies()
    {
        for (int i = 0; i < spawners.Length; i++)
        {
            var enemy = spawners[i];
            enemy.SetActive(true);
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        ActivateEnemies();
        Destroy(gameObject);
    }
}
