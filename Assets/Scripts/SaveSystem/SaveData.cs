using System;
using System.Collections.Generic;

[Serializable]
public class LevelData {
    public string levelName;
    public bool completed;
    public float bestTime;
}

[Serializable]
public class ItemEntry {
    public string itemName;
    public int count;
}

[Serializable]
public class SaveData {
    public List<LevelData> levels = new();
    public List<ItemEntry> items = new();
}
