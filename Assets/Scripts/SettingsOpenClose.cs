using UnityEngine;
using UnityEngine.SceneManagement;
public class SettingsManager : MonoBehaviour
{
    private bool isPaused = false;

    public void OpenSettingsOverlay()
    {
        MusicManager.Instance.PlayButtonPressedSound();
        Time.timeScale = 0f;
        isPaused = true;
        SceneManager.LoadScene("Settings", LoadSceneMode.Additive);
    }

    public void CloseSettingsOverlay()
    {
        MusicManager.Instance.PlayButtonPressedSound();
        Time.timeScale = 1f;
        isPaused = false;
        SceneManager.UnloadSceneAsync("Settings");
    }
}