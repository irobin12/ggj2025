using System;

public static class GameStatesManager
{
    public static Action<States> StateChanged;
    
    public enum States
    {
        StartMenu,
        Credits,
        Wrap,
        Launch,
        Rolling,
        GameOver
    }
    
    public static States currentGameState = States.StartMenu;

    public static void SetGameState(States gameState)
    {
        currentGameState = gameState;
        StateChanged?.Invoke(gameState);
    }

}