using UnityEngine;

public static class Storage
{
    public static readonly StorageData Data = new();
    private const string StorageKey = "storage";

    public static void Save()
    {
        var json = JsonUtility.ToJson(Data);
        PlayerPrefs.SetString(StorageKey, json);
        PlayerPrefs.Save();
    }

    public static void Load()
    {
        var json = PlayerPrefs.GetString(StorageKey);
        JsonUtility.FromJsonOverwrite(json, Data);
    }
}

public class StorageData
{
}