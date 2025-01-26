using System;
using System.Collections;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    private const int DefaultLevelIndex = 0;
    [SerializeField] private GameData gameData;
    [SerializeField] private ThrowableManager throwableManager;
    [SerializeField] private UIManager uiManager;
    [SerializeField] private WrapManager wrapManager;
    private bool firstRun = true;
    
    private void Awake()
    {
        // throwableManager.Initialise(gameData);
        Inputs.Set(gameData.InputData);
        LevelsManager.SetUp(gameData.LevelNames);
        LevelsManager.LoadLevelAdditive(DefaultLevelIndex);
        GameStatesManager.stateChanged += OnStateChanged;
        GameStatesManager.SetGameState(GameStatesManager.States.StartMenu);
        wrapManager.StartClicked += OnStartClicked;
    }

    private void OnStartClicked(Throwable throwable)
    {
        throwableManager.Initialise(gameData, throwable);
        StartCoroutine(ChangeGameState(GameStatesManager.States.Launch));
    }
    
    // Coroutine to avoid the click going through to the launch too early
    private IEnumerator ChangeGameState(GameStatesManager.States newState)
    {
        yield return null;
        GameStatesManager.SetGameState(newState);
    }

    private void OnStateChanged(GameStatesManager.States state)
    {
        switch (state)
        {
            case GameStatesManager.States.StartMenu:
                if(!firstRun) throwableManager.Restart();
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
                firstRun = false;
                throwableManager.ShowThrower(true);
                throwableManager.Restart();
                break;
            case GameStatesManager.States.Rolling:
                uiManager.SetHUD(throwableManager.throwable.icon, throwableManager.throwable.MaxHealthPoints, throwableManager.throwable.AssignedWrap);
                break;
            case GameStatesManager.States.GameOver:
                break;
        }
    }

    void Update()
    {
        if (Inputs.IsKeyUp(Inputs.Menu) && GameStatesManager.CurrentGameState != GameStatesManager.States.StartMenu)
        {
            GameStatesManager.SetGameState(GameStatesManager.States.StartMenu);
        }
        
        switch (GameStatesManager.CurrentGameState)
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
            GameStatesManager.SetGameState(GameStatesManager.States.Launch);
        }
    }
}
