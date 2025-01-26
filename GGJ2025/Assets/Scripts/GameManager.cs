using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static Queue<Throwable> ThrowableQueue = new Queue<Throwable>();
    private const int DefaultLevelIndex = 0;
    [SerializeField] private GameData gameData;
    [SerializeField] private ThrowableManager throwableManager;
    [SerializeField] private UIManager uiManager;
    [SerializeField] private WrapManager wrapManager;
    private bool firstRun = true;
    private int saveCount = 0;
    private int finalScore = 0;
    private int currentThrowableIndex;
    
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
        PrepareLauncher(ThrowableQueue.Dequeue());
    }

    private void PrepareLauncher(Throwable throwable)
    {
        throwableManager.Initialise(gameData, throwable);
        
        throwableManager.ThrowableDamaged -= OnThrowableDamaged;
        throwableManager.ThrowableDied -= OnThrowableDied;
        throwableManager.ThrowableFinished -= OnThrowableFinished;
        
        throwableManager.ThrowableDamaged += OnThrowableDamaged;
        throwableManager.ThrowableDied += OnThrowableDied;
        throwableManager.ThrowableFinished += OnThrowableFinished;

        StartCoroutine(ChangeGameState(GameStatesManager.States.Launch));
    }

    private void OnThrowableFinished(Throwable throwable)
    {
        // Final score: Remaining character health * Remaining bubble-wrap.
        var thisScore = throwable.CurrentHealth * throwable.CurrentWrap;
        finalScore += thisScore;
        saveCount++;

        if (ThrowableQueue.Count > 0)
        {
            PrepareLauncher(ThrowableQueue.Dequeue());
        }
        else
        {
            throwableManager.FinishGame();
        }
    }

    private void OnThrowableDied(Throwable throwable)
    {
    }

    private void OnThrowableDamaged(Throwable throwable)
    {
        uiManager.UpdateHUD(throwable);
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
                uiManager.SetHUD(throwableManager.CurrentThrowable.Icon, throwableManager.CurrentThrowable.MaxHealthPoints, throwableManager.CurrentThrowable.AssignedWrap);
                break;
            case GameStatesManager.States.GameOver:
                uiManager.SetGameOver(saveCount, finalScore);
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
