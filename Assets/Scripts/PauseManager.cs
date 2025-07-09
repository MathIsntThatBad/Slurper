using UnityEngine;
using UnityEngine.SceneManagement;

public class PauseManager : MonoBehaviour
{
    public GameObject pauseMenuUI;
    private bool isPaused = false;

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (isPaused) Resume();
            else Pause();
        }
    }

    public void Resume()
    {
        MusicManager.Instance.PlayButtonPressedSound();
        pauseMenuUI.SetActive(false);
        Time.timeScale = 1f;
        isPaused = false;
    }

    public void Pause()
    {
        MusicManager.Instance.PlayButtonPressedSound();
        pauseMenuUI.SetActive(true);
        Time.timeScale = 0f;
        isPaused = true;
    }
    public void Restart()
    {
        MusicManager.Instance.PlayButtonPressedSound();
        Time.timeScale = 1f; 
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    public void LoadMainMenu()
    {
        MusicManager.Instance.PlayButtonPressedSound();
        Time.timeScale = 1f; 
        SceneManager.LoadScene("LevelSelect");
        MusicManager.Instance?.PlayCategoryMusic("Menu");
    }
}
