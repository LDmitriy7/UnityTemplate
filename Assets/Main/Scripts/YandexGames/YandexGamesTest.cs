using System.Threading.Tasks;
using UnityEngine;

public class YandexGamesTest : MonoBehaviour
{
    private IYandexGames _yandexGames;

    private void Start()
    {
        _yandexGames = YandexGames.Instance;
        _ = Test();
    }

    private async Task Test()
    {
        await _yandexGames.ReadyTask;
        Debug.Log(_yandexGames.GetLang());
    }
}