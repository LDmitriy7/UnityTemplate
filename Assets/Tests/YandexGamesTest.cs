using System.Threading.Tasks;
using UnityEngine;

public class YandexGamesTest : MonoBehaviour
{
    private void Start()
    {
        _ = Test();
    }

    private static async Task Test()
    {
        await YandexGames.ReadyTask;
        Debug.Log($"Lang: {YandexGames.GetLang()}");
        YandexGames.SetLeaderboardScore(1);
        YandexGames.OnGameReady();
        // await TestAd();
        // await TestRewardedAd();
        var shouldRequestFullscreen = YandexGames.ShouldRequestFullscreen();
        if (shouldRequestFullscreen) YandexGames.TryRequestFullscreen();
        Debug.Log($"Time: {YandexGames.GetTime()}");
        YandexGames.SavePlayerData("testData");
        Debug.Log($"Player data: {YandexGames.GetPlayerData()}");
        Debug.Log($"Player id: {YandexGames.GetPlayerId()}");
    }

    private static async Task TestAd()
    {
        await YandexGames.TryShowAd(() => Debug.Log("Ad opened"));
        Debug.Log("Ad closed");
    }

    private static async Task TestRewardedAd()
    {
        await YandexGames.TryShowRewardedAd
        (
            () => Debug.Log("Ad opened"),
            () => Debug.Log("Ad rewarded")
        );
        Debug.Log("Ad closed");
    }
}