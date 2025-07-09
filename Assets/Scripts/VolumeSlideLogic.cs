using UnityEngine;
using UnityEngine.UI;

public class SettingsMenu : MonoBehaviour
{
    public Slider musicSlider;
    public Slider sfxSlider;

    void Start()
    {
        // Initialwerte aus dem MusicManager holen
        float musicVol = MusicManager.Instance.GetMusicVolume();
        float sfxVol = MusicManager.Instance.GetSfxVolume();

        // Slider-Werte setzen, OHNE dass Events ausgelöst werden
        musicSlider.SetValueWithoutNotify(musicVol);
        sfxSlider.SetValueWithoutNotify(sfxVol);

        // Listener registrieren
        musicSlider.onValueChanged.AddListener(SetMusicVolume);
        sfxSlider.onValueChanged.AddListener(SetSFXVolume);
    }

    public void SetMusicVolume(float value)
    {
        MusicManager.Instance.SetMusicVolume(value);
    }

    public void SetSFXVolume(float value)
    {
        MusicManager.Instance.SetSFXVolume(value);
    }
}