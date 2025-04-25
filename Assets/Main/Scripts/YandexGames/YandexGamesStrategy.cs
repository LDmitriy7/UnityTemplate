using System.Runtime.InteropServices;
using UnityEngine;

internal interface IYandexGamesStrategy
{
    string GetLang();
    void SetLeaderboardScore(long score);
}

internal class DefaultYandexGamesStrategy : IYandexGamesStrategy
{
    public string GetLang() => YandexGames_GetLang();
    public void SetLeaderboardScore(long score) => YandexGames_SetLeaderboardScore(score);

    [DllImport("__Internal")]
    private static extern string YandexGames_GetLang();

    [DllImport("__Internal")]
    private static extern void YandexGames_SetLeaderboardScore(long score);
}

internal class MockYandexGamesStrategy : IYandexGamesStrategy
{
    public string GetLang() => "en";

    public void SetLeaderboardScore(long score)
    {
        Debug.Log($"Set leaderboard score: {score}");
    }
}