using System.Collections;
using UnityEngine.Events;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;
    private bool m_isPaused = false;
    public static bool InputLocked { get; private set; } = false;
    public static void SetInputLocked(bool v) => InputLocked = v;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
            return;
        }

        PauseMenuController.OnPause += HandlePause;
        PauseMenuController.OnResume += HandleResume;
        SceneManager.sceneLoaded += OnSceneLoaded;
    }

    private void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        SetInputLocked(false);
        Time.timeScale = 1f;
    }

    public void PlayerDied()
    {
        StartCoroutine(WaitAndLoadLoseScene());
    }

    private IEnumerator WaitAndLoadLoseScene()
    {
        yield return new WaitForSeconds(0.50f);
        SceneManager.LoadScene("Perder");
    }

    private void OnDestroy()
    {
        SceneManager.sceneLoaded -= OnSceneLoaded;
        PauseMenuController.OnPause -= HandlePause;
        PauseMenuController.OnResume -= HandleResume;
    }

    private void HandlePause()
    {
        m_isPaused = true;
        Time.timeScale = 0f;
    }

    private void HandleResume()
    {
        m_isPaused = false;
        Time.timeScale = 1f;
    }
}