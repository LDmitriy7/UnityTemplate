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
        Debug.Log(YandexGames.GetLang());
        YandexGames.SetLeaderboardScore(1);
    }
}