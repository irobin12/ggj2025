using System;

public static class GameStatesManager
{
    public static Action<States> stateChanged;
    
    public enum States
    {
        StartMenu,
        Credits,
        Wrap,
        Launch,
        Rolling,
        GameOver
    }
    
    public static States CurrentGameState {get; private set;}

    public static void SetGameState(States gameState)
    {
        CurrentGameState = gameState;
        stateChanged?.Invoke(gameState);
    }

}