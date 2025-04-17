using UnityEngine;

public static class Storage
{
    readonly public static StorageData Data = new();
    private const string STORAGE_KEY = "storage";

    public static void Save()
    {
        var json = JsonUtility.ToJson(Data);
        PlayerPrefs.SetString(STORAGE_KEY, json);
        PlayerPrefs.Save();
    }

    public static void Load()
    {
        var json = PlayerPrefs.GetString(STORAGE_KEY);
        JsonUtility.FromJsonOverwrite(json, Data);
    }
}

public class StorageData
{
}