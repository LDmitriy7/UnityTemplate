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
        if (_adShowTcs != null) throw new("Ad is already requested");
        _adShowTcs = new();
        _onAdOpen = onOpen;
        Js_TryShowAd();
        return _adShowTcs.Task;
    }

    public Task TryShowRewardedAd(Action onOpen, Action onReward)
    {
        if (_adShowTcs != null) throw new("Ad is already requested");
        _adShowTcs = new();
        _onAdOpen = onOpen;
        _onAdReward = onReward;
        Js_TryShowRewardedAd();
        return _adShowTcs.Task;
    }

    [UsedImplicitly]
    private void Js_OnReady()
    {
        _readyTcs.SetResult(true);
    }

    [UsedImplicitly]
    private void Js_OnAdOpen()
    {
        _onAdOpen?.Invoke();
        _onAdOpen = null;
    }

    [UsedImplicitly]
    private void Js_OnAdReward()
    {
        _onAdReward?.Invoke();
        _onAdReward = null;
    }

    [UsedImplicitly]
    private void Js_OnAdClose()
    {
        _adShowTcs?.SetResult(true);
        _adShowTcs = null;
    }

    [DllImport("__Internal")]
    private static extern void Js_TryShowAd();

    [DllImport("__Internal")]
    private static extern void Js_TryShowRewardedAd();
}