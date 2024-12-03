using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelManager1 : MonoBehaviour
{
    private static LevelManager1 instance;
    public static LevelManager1 Instance { get { return instance; } }

    [SerializeField] public GameObject[] puertaCerrada;

    private int totalSpawners = 0;
    private int totalEnemies = 0;

    void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
    }

    public void RegisterSpawner()
    {
        totalSpawners++;
    }

    public void RegisterEnemy()
    {
        totalEnemies++;
    }

    public void UnregisterSpawner()
    {
        totalSpawners--;

        CheckWinCondition();
    }

    public void UnregisterEnemy()
    {
        totalEnemies--;

        CheckWinCondition();
    }

    private void ActivatePuerta()
    {
        for (int i = 0; i < puertaCerrada.Length; i++)
        {
            var puertaAbierta = puertaCerrada[i];
            puertaAbierta.SetActive(true);
        }
    }

    private void CheckWinCondition()
    {
        GameObject puertaCerrada = GameObject.Find("PuertaCerrada");

        if (totalSpawners == 0 && totalEnemies == 0)
        {
            if (puertaCerrada != null)
            {
                puertaCerrada.SetActive(false);
            }

            ActivatePuerta();
        }
    }
}