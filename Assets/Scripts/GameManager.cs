using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;

    public static bool CanTogglePause = true;
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

        SceneManager.sceneLoaded += OnSceneLoaded;
    }

    private void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        SetInputLocked(false);
        Time.timeScale = 1f;
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape) && CanTogglePause)
        {
            if (!m_isPaused)
            {
                PauseGame();
            }
            else
            {
                ResumeGame();
            }
            
        }
    }

    private void PauseGame()
    {
        Time.timeScale = 0f;
        m_isPaused = true;
    }

    private void ResumeGame()
    {
        Time.timeScale = 1f;
        m_isPaused = false;
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
    }
}