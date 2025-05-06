using System;
using System.Runtime.InteropServices;
using System.Threading.Tasks;
using UnityEngine;

internal interface IStrategy
{
    Task ReadyTask { get; }
    string GetLang();
    void SetLeaderboardScore(long score);
    void OnGameReady();
    Task TryShowAd(Action onOpen);
    Task TryShowRewardedAd(Action onOpen, Action onReward);
    void TryRequestFullscreen();
    bool ShouldRequestFullscreen();
    long GetTime();
    void SavePlayerData(string data);
    string GetPlayerData();
    string GetPlayerId();
}

internal class DefaultStrategy : IStrategy
{
    private readonly Manager _manager;

    public DefaultStrategy(Manager manager)
    {
        _manager = manager;
    }

    public Task ReadyTask => _manager.ReadyTask;

    public string GetLang() => YandexGames_GetLang();

    public void SetLeaderboardScore(long score) => YandexGames_SetLeaderboardScore(score);

    public void OnGameReady() => YandexGames_OnGameReady();

    public async Task TryShowAd(Action onOpen)
    {
        await _manager.TryShowAd(onOpen);
    }

    public async Task TryShowRewardedAd(Action onOpen, Action onReward)
    {
        await _manager.TryShowRewardedAd(onOpen, onReward);
    }

    public void TryRequestFullscreen() => YandexGames_TryRequestFullscreen();

    public bool ShouldRequestFullscreen() => YandexGames_ShouldRequestFullscreen() == 1;

    public long GetTime() => (long)YandexGames_GetTime();

    public void SavePlayerData(string data) => YandexGames_SavePlayerData(data);

    public string GetPlayerData() => YandexGames_GetPlayerData();

    public string GetPlayerId() => YandexGames_GetPlayerId();

    [DllImport("__Internal")]
    private static extern string YandexGames_GetLang();

    [DllImport("__Internal")]
    private static extern void YandexGames_SetLeaderboardScore(double score);

    [DllImport("__Internal")]
    private static extern void YandexGames_OnGameReady();

    [DllImport("__Internal")]
    private static extern void YandexGames_TryRequestFullscreen();

    [DllImport("__Internal")]
    private static extern int YandexGames_ShouldRequestFullscreen();

    [DllImport("__Internal")]
    private static extern double YandexGames_GetTime();

    [DllImport("__Internal")]
    private static extern void YandexGames_SavePlayerData(string data);

    [DllImport("__Internal")]
    private static extern string YandexGames_GetPlayerData();

    [DllImport("__Internal")]
    private static extern string YandexGames_GetPlayerId();
}

internal class MockStrategy : IStrategy
{
    private const string DataKey = "data";

    public Task ReadyTask => Task.CompletedTask;

    public string GetLang() => "en";

    public void SetLeaderboardScore(long score)
    {
        Log($"Leaderboard score set: {score}");
    }

    public void OnGameReady()
    {
        Log("Game is ready");
    }

    public Task TryShowAd(Action onOpen)
    {
        onOpen?.Invoke();
        Log("Ad shown");
        return Task.CompletedTask;
    }

    public Task TryShowRewardedAd(Action onOpen = null, Action onReward = null)
    {
        onOpen?.Invoke();
        onReward?.Invoke();
        Log("Rewarded ad shown");
        return Task.CompletedTask;
    }

    public void TryRequestFullscreen()
    {
        Log("Fullscreen requested");
    }

    public bool ShouldRequestFullscreen()
    {
        return true;
    }

    public long GetTime()
    {
        return 0;
    }

    public void SavePlayerData(string data)
    {
        PlayerPrefs.SetString(DataKey, data);
        PlayerPrefs.Save();
        Log($"Data saved: {data}");
    }

    public string GetPlayerData()
    {
        return PlayerPrefs.GetString(DataKey);
    }

    public string GetPlayerId()
    {
        return "0";
    }

    private static void Log(string message)
    {
        Debug.Log($"[YandexGames] {message}");
    }
}