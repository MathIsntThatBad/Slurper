using UnityEngine;

public class UnlockManager : MonoBehaviour
{
    public static UnlockManager Instance;

    void Awake()
    {
        // Singleton
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject); 
        }
        else
        {
            Destroy(gameObject); 
        }
    }

}
