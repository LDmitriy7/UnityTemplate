using System.Threading.Tasks;
using UnityEngine;

public static class YandexGames
{
    private static YandexGamesManager _manager;
    private static IYandexGamesStrategy _strategy;

    [RuntimeInitializeOnLoadMethod(RuntimeInitializeLoadType.BeforeSceneLoad)]
    private static void Init()
    {
        CreateManager();

        if (Application.isEditor)
        {
            _strategy = new MockYandexGamesStrategy();
            _manager.OnJsReady();
        }
        else
        {
            _strategy = new DefaultYandexGamesStrategy();
        }
    }

    private static void CreateManager()
    {
        var managerObject = new GameObject("YandexGamesManager");
        Object.DontDestroyOnLoad(managerObject);
        _manager = managerObject.AddComponent<YandexGamesManager>();
    }

    public static Task ReadyTask => _manager.ReadyTask;
    public static string GetLang() => _strategy.GetLang();
    public static void SetLeaderboardScore(long score) => _strategy.SetLeaderboardScore(score);
}