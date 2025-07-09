using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    [SerializeField] private AudioSource audioSource;

    public void StartGame()
    {
        MusicManager.Instance.PlayButtonPressedSound();
        SceneManager.LoadScene("LevelSelect");
    }

    public void ExitGame()
    {
        MusicManager.Instance.PlayButtonPressedSound();
        Application.Quit();
        Debug.Log("Exiting"); 
    }
}
