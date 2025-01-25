public static class GameStatesManager
{
    public enum GameState
    {
        StartMenu,
        Launch,
        Rolling,
        GameOver
    }
    
    public static GameState CurrentGameState = GameState.Launch;

    public static void SetGameState(GameState gameState)
    {
        CurrentGameState = gameState;
    }

}