using System;
using System.Collections;
using UnityEngine;

public class UIManager : MonoBehaviour
{
    [SerializeField] private StartScreen startScreen;
    [SerializeField] private CreditsScreen creditsScreen;
    [SerializeField] private WrapScreen wrapScreen;
    [SerializeField] private LaunchScreen launchScreen;
    [SerializeField] private RollingScreen rollingScreen;
    [SerializeField] private GameOverScreen gameOverScreen;

    private void Awake()
    {
        GameStatesManager.StateChanged += OnStateChanged;
        startScreen.WrapClicked += OnWrapClicked;
    }

    private void OnStateChanged(GameStatesManager.States state)
    {
        switch (state)
        {
            case GameStatesManager.States.StartMenu:
                startScreen.gameObject.SetActive(true);
                creditsScreen.gameObject.SetActive(false);
                wrapScreen.gameObject.SetActive(false);
                launchScreen.gameObject.SetActive(false);
                rollingScreen.gameObject.SetActive(false);
                gameOverScreen.gameObject.SetActive(false);
                break;
            case GameStatesManager.States.Credits:
                startScreen.gameObject.SetActive(false);
                creditsScreen.gameObject.SetActive(true);
                wrapScreen.gameObject.SetActive(false);
                launchScreen.gameObject.SetActive(false);
                rollingScreen.gameObject.SetActive(false);
                gameOverScreen.gameObject.SetActive(false);
                break;
            case GameStatesManager.States.Wrap:
                startScreen.gameObject.SetActive(false);
                creditsScreen.gameObject.SetActive(false);
                wrapScreen.gameObject.SetActive(true);
                launchScreen.gameObject.SetActive(false);
                rollingScreen.gameObject.SetActive(false);
                gameOverScreen.gameObject.SetActive(false);
                break;
            case GameStatesManager.States.Launch:
                startScreen.gameObject.SetActive(false);
                creditsScreen.gameObject.SetActive(false);
                wrapScreen.gameObject.SetActive(false);
                launchScreen.gameObject.SetActive(true);
                rollingScreen.gameObject.SetActive(false);
                gameOverScreen.gameObject.SetActive(false);
                break;
            case GameStatesManager.States.Rolling:
                startScreen.gameObject.SetActive(false);
                creditsScreen.gameObject.SetActive(false);
                wrapScreen.gameObject.SetActive(false);
                launchScreen.gameObject.SetActive(false);
                rollingScreen.gameObject.SetActive(true);
                gameOverScreen.gameObject.SetActive(false);
                break;
            case GameStatesManager.States.GameOver:
                startScreen.gameObject.SetActive(false);
                creditsScreen.gameObject.SetActive(false);
                wrapScreen.gameObject.SetActive(false);
                launchScreen.gameObject.SetActive(false);
                rollingScreen.gameObject.SetActive(false);
                gameOverScreen.gameObject.SetActive(true);
                break;
            default:
                throw new ArgumentOutOfRangeException(nameof(state), state, null);
        }
    }

    private void OnWrapClicked()
    {
        startScreen.gameObject.SetActive(false);
        StartCoroutine(ChangeGameState());
    }

    // Coroutine to avoid the click going through to the launch too early
    private IEnumerator ChangeGameState()
    {
        yield return null;
        GameStatesManager.SetGameState(GameStatesManager.States.Launch);
    }

    private void OnDestroy()
    {
        startScreen.WrapClicked -= OnWrapClicked;
        GameStatesManager.StateChanged -= OnStateChanged;
    }
}