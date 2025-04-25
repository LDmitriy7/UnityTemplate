using System.Threading.Tasks;
using UnityEngine;

internal class YandexGamesManager : MonoBehaviour
{
    private readonly TaskCompletionSource<bool> _readyTcs = new();

    public Task ReadyTask => _readyTcs.Task;

    // Called from js
    public void OnJsReady()
    {
        _readyTcs.SetResult(true);
    }
}