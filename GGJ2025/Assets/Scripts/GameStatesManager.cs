public static class GameStatesManager
{
    public enum States
    {
        StartMenu,
        Launch,
        Rolling,
        GameOver
    }
    
    public static States currentGameState = States.StartMenu;

    public static void SetGameState(States gameState)
    {
        currentGameState = gameState;
    }

}