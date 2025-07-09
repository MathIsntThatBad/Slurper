using UnityEngine;
using UnityEngine.SceneManagement;

public class SettingsOverlayUI : MonoBehaviour
{
    public void OnCloseSettings()
    {
        MusicManager.Instance.PlayButtonPressedSound();
        Time.timeScale = 1f;
        SceneManager.UnloadSceneAsync("Settings");
    }
}