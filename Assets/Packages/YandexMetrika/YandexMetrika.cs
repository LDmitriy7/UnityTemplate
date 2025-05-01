using System.Runtime.InteropServices;
using UnityEngine;

public static class YandexMetrika
{
    public static void Send(string goal)
    {
        if (Application.isEditor) Log($"Goal reached: {goal}");
        else YandexMetrika_Send(goal);
    }

    private static void Log(string message)
    {
        Debug.Log($"[YandexMetrika] {message}");
    }

    [DllImport("__Internal")]
    private static extern void YandexMetrika_Send(string goal);
}