using System.IO;
using UnityEngine;

public static class SaveManager {
    private static string path = Application.persistentDataPath + "/save.json";

    public static void Save(SaveData data) {
        string json = JsonUtility.ToJson(data, true);
        File.WriteAllText(path, json);
    }

    public static SaveData Load() {
        if (File.Exists(path)) {
            Debug.Log(path);
            string json = File.ReadAllText(path);
            return JsonUtility.FromJson<SaveData>(json);
        }
        Debug.Log(path);
        return new SaveData();
    }

    public static void DeleteSave() {
        if (File.Exists(path)) File.Delete(path);
    }
}

