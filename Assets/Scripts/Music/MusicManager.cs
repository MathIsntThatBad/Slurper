using UnityEngine;

public class MusicManager : MonoBehaviour
{
    public static MusicManager Instance;

    public AudioClip forestMusic;
    public AudioClip beachMusic;
    public AudioClip caveMusic;
    public AudioClip mountainMusic;
    public AudioClip menu;
    public AudioClip bossMusic;
    public AudioClip afterBossMusic;
    public AudioClip takeDamage;
    public AudioClip buttonPressed;
    public AudioClip jumpSelector;
    public AudioClip gameOver;
    public AudioClip coinGathered;
    public AudioClip otherGathered;
    public AudioClip stageComplete;
    public AudioClip levelComplete;
    [Range(0f, 1f)] public float musicVolume = 0.5f;
    [Range(0f, 1f)] public float sfxVolume = 0.7f;

    private AudioSource musicSource;
    private AudioSource sfxSource;
    private string currentCategory;

    void Awake()
    {
        // Singleton
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject); // Erhalte Objekt über Szenen hinweg

            musicSource = gameObject.AddComponent<AudioSource>();
            musicSource.loop = true;
            musicSource.ignoreListenerPause = true;
            musicSource.playOnAwake = false;
            musicSource.clip = menu;
            musicSource.volume = musicVolume;
            musicSource.Play();



            // SFX-Quelle
            sfxSource = gameObject.AddComponent<AudioSource>();
            sfxSource.loop = false;
            sfxSource.playOnAwake = false;
            sfxSource.volume = sfxVolume;
        }
        else
        {
            Destroy(gameObject); // Verhindere doppelte Instanzen
        }
    }
    public void SetMusicVolume(float volume)
    {
        musicVolume = volume;
        musicSource.volume = musicVolume;
    }

    public void SetSFXVolume(float volume)
    {
        sfxVolume = volume;
        sfxSource.volume = sfxVolume;
    }

    public void PlayCategoryMusic(string category)
    {
        if (currentCategory == category) return;
        currentCategory = category;

        AudioClip selectedClip = null;

        switch (category)
        {
            case "Forest": selectedClip = forestMusic; break;
            case "Beach": selectedClip = beachMusic; break;
            case "Cave": selectedClip = caveMusic; break;
            case "Mountain": selectedClip = mountainMusic; break;
            case "Menu": selectedClip = menu; break;
            case "Boss": selectedClip = bossMusic; break;
            case "AfterBoss": selectedClip = afterBossMusic; break;
        }

        if (selectedClip != null && musicSource.clip != selectedClip)
        {
            musicSource.Stop();
            musicSource.clip = selectedClip;
            musicSource.volume = musicVolume;
            musicSource.Play();
        }
    }

    public void StopMusic()
    {
        musicSource.Stop();
        currentCategory = null;
    }
    public void PlayTakeDamageSound() => PlaySFX(takeDamage);
    public void PlayButtonPressedSound() => PlaySFX(buttonPressed);
    public void PlayJumpSelectorSound() => PlaySFX(jumpSelector);
    public void PlayGameOverSound() => PlaySFX(gameOver);
    public void PlayCoinGatheredSound() => PlaySFX(coinGathered);
    public void PlayOtherGatheredSound() => PlaySFX(otherGathered);
    public void PlayStageCompleteSound() => PlaySFX(stageComplete);
    public void PlayLevelCompleteSound() => PlaySFX(levelComplete);

    private void PlaySFX(AudioClip clip)
    {
        if (clip != null)
        {
            sfxSource.PlayOneShot(clip, sfxVolume);
        }
    }
    public float GetMusicVolume() => musicVolume;
    public float GetSfxVolume() => sfxVolume;
}
