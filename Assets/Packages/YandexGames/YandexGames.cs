using System;
using System.Threading.Tasks;
using UnityEngine;
using Object = UnityEngine.Object;

public static class YandexGames
{
    private static IStrategy _strategy;

    [RuntimeInitializeOnLoadMethod(RuntimeInitializeLoadType.BeforeSceneLoad)]
    private static void Init()
    {
        if (Application.isEditor)
        {
            _strategy = new MockStrategy();
        }
        else
        {
            var manager = CreateManager();
            _strategy = new DefaultStrategy(manager);
        }
    }

    public static Task ReadyTask => _strategy.ReadyTask;

    public static string GetLang() => _strategy.GetLang();

    public static void SetLeaderboardScore(long score) => _strategy.SetLeaderboardScore(score);

    public static void OnGameReady() => _strategy.OnGameReady();

    public static Task TryShowAd(Action onOpen = null) => _strategy.TryShowAd(onOpen);

    public static Task TryShowRewardedAd(Action onOpen = null, Action onReward = null) =>
        _strategy.TryShowRewardedAd(onOpen, onReward);

    public static void TryRequestFullscreen() => _strategy.TryRequestFullscreen();

    public static bool ShouldRequestFullscreen() => _strategy.ShouldRequestFullscreen();

    public static long GetTime() => _strategy.GetTime();
    
    public static void SavePlayerData(string data) => _strategy.SavePlayerData(data);

    public static string GetPlayerData() => _strategy.GetPlayerData();

    public static string GetPlayerId() => _strategy.GetPlayerId();

    private static Manager CreateManager()
    {
        var managerObject = new GameObject("YandexGamesManager");
        Object.DontDestroyOnLoad(managerObject);
        return managerObject.AddComponent<Manager>();
    }
}