using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelManager2 : MonoBehaviour
{
    private static LevelManager2 instance;
    public static LevelManager2 Instance { get { return instance; } }

    [SerializeField] public GameObject[] puertaCerrada;

    private int totalMinions = 0;

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

    public void RegisterMinion()
    {
        totalMinions++;
    }

    public void UnregisterMinion()
    {
        totalMinions--;
        CheckWinCondition();
    }

    private void ActivatePuertas()
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

        if (totalMinions == 0)
        {
            if (puertaCerrada != null)
            {
                puertaCerrada.SetActive(false);
            }

            ActivatePuertas();
        }
    }
}