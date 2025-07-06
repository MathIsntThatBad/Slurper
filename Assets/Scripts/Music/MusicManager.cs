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
    public float volume = 0.5f;

    private AudioSource audioSource;
    private string currentCategory;

    void Awake()
    {
        // Singleton
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject); // Erhalte Objekt über Szenen hinweg

            audioSource = gameObject.AddComponent<AudioSource>();
            audioSource.loop = true;
            audioSource.playOnAwake = false;
            audioSource.clip = menu;
            audioSource.volume = volume;
            audioSource.Play();
        }
        else
        {
            Destroy(gameObject); // Verhindere doppelte Instanzen
        }
    }

    public void PlayCategoryMusic(string category)
    {
        if (currentCategory == category) return; // Musik läuft schon

        currentCategory = category;

        switch (category)
        {
            case "Forest":
                if (audioSource.clip == forestMusic && audioSource.isPlaying) return;
                audioSource.Stop(); 
                audioSource.clip = forestMusic;
                break;
            case "Beach":
                if(audioSource.clip == beachMusic && audioSource.isPlaying) return;
                audioSource.Stop(); 
                audioSource.clip = beachMusic;
                break;
            case "Cave":
                if(audioSource.clip == caveMusic && audioSource.isPlaying) return;
                audioSource.Stop();
                audioSource.clip = caveMusic;
                break;
            case "Mountain":
                if(audioSource.clip == mountainMusic && audioSource.isPlaying) return;
                audioSource.Stop(); 
                audioSource.clip = mountainMusic;
                break;
            case "Menu":
                if(audioSource.clip == menu && audioSource.isPlaying) return;
                audioSource.Stop(); 
                audioSource.clip = menu;
                break;
            case "Boss":
                if(audioSource.clip == mountainMusic && audioSource.isPlaying) return;
                audioSource.Stop(); 
                audioSource.clip = mountainMusic;
                break;
            default:
                audioSource.clip = null;
                break;
        }

        if (audioSource.clip != null)
            audioSource.Play();
    }

    public void StopMusic()
    {
        audioSource.Stop();
        currentCategory = null;
    }
}
