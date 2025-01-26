using System;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    private const int DefaultLevelIndex = 0;
    [SerializeField] private GameData gameData;
    [SerializeField] private ThrowableManager throwableManager;
    [SerializeField] private UIManager uiManager;
    [SerializeField] private WrapManager wrapManager;
    
    private void Awake()
    {
        // throwableManager.Initialise(gameData);
        Inputs.Set(gameData.InputData);
        LevelsManager.SetUp(gameData.LevelNames);
        LevelsManager.LoadLevelAdditive(DefaultLevelIndex);
        GameStatesManager.StateChanged += OnStateChanged;
        GameStatesManager.SetGameState(GameStatesManager.States.StartMenu);
        wrapManager.StartClicked += OnStartClicked;
    }

    private void OnStartClicked(Throwable throwable)
    {
        throwableManager.Initialise(gameData, throwable);
        GameStatesManager.SetGameState(GameStatesManager.States.Launch);
    }

    private void OnStateChanged(GameStatesManager.States state)
    {
        switch (state)
        {
            case GameStatesManager.States.StartMenu:
                throwableManager.ShowThrower(false);
                wrapManager.gameObject.SetActive(false);
                break;
            case GameStatesManager.States.Credits:
                break;
            case GameStatesManager.States.Wrap:
                wrapManager.gameObject.SetActive(true);
                wrapManager.Initialise(gameData);
                break;
            case GameStatesManager.States.Launch:
                throwableManager.ShowThrower(true);
                throwableManager.Restart();
                break;
            case GameStatesManager.States.Rolling:
                break;
            case GameStatesManager.States.GameOver:
                break;
        }
    }

    void Update()
    {
        if (Inputs.IsKeyUp(Inputs.Menu) && GameStatesManager.currentGameState != GameStatesManager.States.StartMenu)
        {
            GameStatesManager.SetGameState(GameStatesManager.States.StartMenu);
        }
        
        switch (GameStatesManager.currentGameState)
        {
            case GameStatesManager.States.Launch:
            {
                UpdateLaunch();

                break;
            }

            case GameStatesManager.States.Rolling:
            case GameStatesManager.States.GameOver:
            {
                Restart();

                break;
            }
        }
    }

    private void UpdateLaunch()
    {
        throwableManager.UpdateLaunch();
    }

    private void Restart()
    {
        if (Inputs.IsKeyUp(Inputs.Restart))
        {
            throwableManager.Restart();
        }
    }
}
