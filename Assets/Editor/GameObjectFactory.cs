using UnityEngine;
using UnityEngine.UI;
using UnityEditor;
using System;

public class GameObjectFactory
{
    [MenuItem("GameObject/UI/Background", false)]
    public static void CreateBackground(MenuCommand command)
    {
        var background = CreateGameObject("Background", command);
        background.AddComponent<Image>();

        var transform = background.transform as RectTransform;

        transform.anchorMin = Vector2.zero;
        transform.anchorMax = Vector2.one;
        transform.offsetMin = Vector2.zero;
        transform.offsetMax = Vector2.zero;
    }

    [MenuItem("GameObject/UI/RoundedRect", false)]
    public static void CreateRoundedRect(MenuCommand command)
    {
        var spriteName = "RoundedRect";
        Sprite sprite = Resources.Load<Sprite>(spriteName);
        if (!sprite) throw new Exception($"Sprite '{spriteName}' not found in Resources!");

        var rect = CreateGameObject(spriteName, command);
        var image = rect.AddComponent<Image>();

        image.sprite = sprite;
        image.type = Image.Type.Sliced;
        image.pixelsPerUnitMultiplier = 20;
    }

    private static GameObject CreateGameObject(string name, MenuCommand command)
    {
        GameObject gameObject = new(name);
        GameObjectUtility.SetParentAndAlign(gameObject, command.context as GameObject);
        Undo.RegisterCreatedObjectUndo(gameObject, "Create " + gameObject.name);
        Selection.activeObject = gameObject;
        return gameObject;
    }
}