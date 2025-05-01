using System;
using System.Runtime.InteropServices;
using System.Threading.Tasks;
using JetBrains.Annotations;
using UnityEngine;

internal class Manager : MonoBehaviour
{
    private readonly TaskCompletionSource<bool> _readyTcs = new();
    private TaskCompletionSource<bool> _adShowTcs;
    private Action _onAdOpen;
    private Action _onAdReward;

    public Task ReadyTask => _readyTcs.Task;

    public Task TryShowAd(Action onOpen)
    {
        BeforeAdShow();
        _onAdOpen = onOpen;
        YandexGames_TryShowAd();
        return _adShowTcs.Task;
    }

    public Task TryShowRewardedAd(Action onOpen, Action onReward)
    {
        BeforeAdShow();
        _onAdOpen = onOpen;
        _onAdReward = onReward;
        YandexGames_TryShowRewardedAd();
        return _adShowTcs.Task;
    }

    private void BeforeAdShow()
    {
        if (_adShowTcs != null) throw new("Ad is already requested");
        _adShowTcs = new();
    }

    [UsedImplicitly]
    private void OnReady()
    {
        _readyTcs.SetResult(true);
    }

    [UsedImplicitly]
    private void OnAdOpen()
    {
        _onAdOpen?.Invoke();
    }

    [UsedImplicitly]
    private void OnAdReward()
    {
        _onAdReward?.Invoke();
    }

    [UsedImplicitly]
    private void OnAdClose()
    {
        _adShowTcs?.SetResult(true);
        _adShowTcs = null;
        _onAdOpen = null;
        _onAdReward = null;
    }

    [DllImport("__Internal")]
    private static extern void YandexGames_TryShowAd();

    [DllImport("__Internal")]
    private static extern void YandexGames_TryShowRewardedAd();
}