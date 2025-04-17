using UnityEngine.SceneManagement;

public static class SceneLoader
{
    public static void LoadMainScene() => LoadScene("MainScene");

    private static void LoadScene(string sceneName)
    {
        SceneManager.LoadScene(sceneName);
    }
}
