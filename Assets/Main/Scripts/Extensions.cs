using System;
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
}