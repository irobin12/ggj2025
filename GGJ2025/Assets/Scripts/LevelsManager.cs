using UnityEngine.SceneManagement;

public static class LevelsManager
{
    private static string[] Levels { get; set; }
    public static int currentLevelIndex;
    public static string currentLevelName;

    public static void SetUp(string[] levelNames)
    {
        Levels = levelNames;
    }
    
    public static void LoadLevelAdditive(int index)
    {
        SceneManager.LoadScene(Levels[index], LoadSceneMode.Additive);
        currentLevelName = Levels[index];
    }

    public static void SelectLevel(int levelIndex)
    {
        if (levelIndex < 0) levelIndex = Levels.Length - 1;
        if (levelIndex >= Levels.Length) levelIndex = 0;
        
        currentLevelIndex = levelIndex;
        SceneManager.UnloadSceneAsync(currentLevelName);
        LoadLevelAdditive(levelIndex);
    }
}