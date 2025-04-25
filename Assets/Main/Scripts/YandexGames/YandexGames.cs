using System.Runtime.InteropServices;
using System.Threading.Tasks;
using UnityEngine;

// TODO: load before scene

public interface IYandexGames
{
    Task ReadyTask { get; }
    string GetLang();
}

public class YandexGames : MonoBehaviour, IYandexGames
{
    public static YandexGames Instance { get; private set; }
    
    private readonly TaskCompletionSource<bool> _readyTcs = new();
    private IStrategy _strategy;

    public Task ReadyTask => _readyTcs.Task;
    public string GetLang() => _strategy.GetLang();
    
    private void Awake()
    {
        if (Instance == null)
        {
            Init();
        }
        else
        {
            Destroy(gameObject);
        }
    }

    private void Init()
    {
        Instance = this;
        DontDestroyOnLoad(gameObject);
        if (Application.isEditor)
        {
            _strategy = new MockStrategy();
            OnReady();
        }
        else
        {
            _strategy = new DefaultStrategy();
        }
    }
    
    // Called from js
    private void OnReady()
    {
        _readyTcs.SetResult(true);
    }
}

internal interface IStrategy
{
    string GetLang();
}

internal class DefaultStrategy : IStrategy
{
    public string GetLang() => YandexGames_GetLang();

    [DllImport("__Internal")]
    private static extern string YandexGames_GetLang();
}

internal class MockStrategy : IStrategy
{
    public string GetLang() => "en";
}