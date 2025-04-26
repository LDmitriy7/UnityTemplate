using System;
using System.Collections;
using System.Threading.Tasks;
using UnityEngine;

public static class Extensions
{
    public static void LocalTranslate(this Transform transform, Vector3 translation, Space space)
    {
        switch (space)
        {
            case Space.Self:
                translation = transform.localRotation * translation;
                break;
            case Space.World:
                break;
            default:
                throw new Exception($"Unknown space: {space}");
        }

        transform.localPosition += translation;
    }

    public static Task Sleep(this MonoBehaviour behaviour, float duration)
    {
        var tcs = new TaskCompletionSource<bool>();
        behaviour.StartCoroutine(SleepCoroutine(duration, tcs));
        return tcs.Task;
    }

    private static IEnumerator SleepCoroutine(float seconds, TaskCompletionSource<bool> tcs)
    {
        yield return new WaitForSeconds(seconds);
        tcs.SetResult(true);
    }
}