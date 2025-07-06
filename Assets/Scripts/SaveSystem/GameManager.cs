using UnityEngine;
using System.Collections.Generic;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;

    private SaveData save;

    void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject); // bleibt bei Szenenwechsel erhalten
        }
        else
        {
            Destroy(gameObject);
        }
    }

    void Start()
    {
        //Resets on every start 
        ResetProgress();
        //
        save = SaveManager.Load();
        Debug.Log("Spielstand geladen.");
    }

    public void CompleteLevel(string levelName, float timeTaken)
    {
        var level = save.levels.Find(l => l.levelName == levelName);

        if (level == null)
        {
            // Noch kein Eintrag → neuen erstellen
            level = new LevelData
            {
                levelName = levelName,
                completed = true,
                bestTime = timeTaken
            };
            save.levels.Add(level);
        }
        else
        {
            // Schon vorhanden → aktualisieren
            level.completed = true;

            // Nur überschreiben, wenn Zeit besser ist
            if (level.bestTime == 0 || timeTaken < level.bestTime)
                level.bestTime = timeTaken;
        }

        SaveManager.Save(save);
    }

    public void CollectItem(string itemName, int amount)
    {
        var item = save.items.Find(i => i.itemName == itemName);
        if (item == null)
        {
            item = new ItemEntry { itemName = itemName, count = amount };
            save.items.Add(item);
        }
        else
        {
            item.count += amount;
        }
        SaveManager.Save(save);
    }

    public void ResetProgress()
    {
        SaveManager.DeleteSave();
        save = new SaveData();
        Debug.Log("Spielstand zurückgesetzt.");
    }


    public SaveData GetSaveData() => save; // Optional für UI

    public bool HasItem(string itemName, int requiredAmount)
    {
        var item = save.items.Find(i => i.itemName == itemName);
        return item != null && item.count >= requiredAmount;
    }
    public bool IsLevelCompleted(string levelName)
    {
        var level = save.levels.Find(l => l.levelName == levelName);
        return level != null && level.completed;
    }
    public float GetLevelBestTime(string levelName)
    {
        var level = save.levels.Find(l => l.levelName == levelName);
        return level != null ? level.bestTime : 0f;
    }
    public int GetItemCount(string itemName)
    {
        var item = save.items.Find(i => i.itemName == itemName);
        return item != null ? item.count : 0;
    }
}

