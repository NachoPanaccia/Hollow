using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelManager3 : MonoBehaviour
{
    private static LevelManager3 instance;
    public static LevelManager3 Instance { get { return instance; } }

    private bool isBossAlive = true;

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

    public void BossDefeated()
    {
        isBossAlive = false;
        CheckWinCondition();
    }

    private void CheckWinCondition()
    {
        if (isBossAlive == false)
        {
            StartCoroutine(WaitAndLoadGanarScene());
        }
    }


    private IEnumerator WaitAndLoadGanarScene()
    {
        yield return new WaitForSeconds(3f);

        SceneManager.LoadScene("Ganar");
    }
}