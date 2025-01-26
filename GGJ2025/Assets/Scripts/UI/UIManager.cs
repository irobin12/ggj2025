using System.Collections;
using UnityEngine;

public class UIManager : MonoBehaviour
{
    [SerializeField] private StartScreen startScreen;
    [SerializeField] private RectTransform creditsScreen;
    [SerializeField] private RectTransform throwableSelectScreen;
    [SerializeField] private RectTransform launchScreen;
    [SerializeField] private RectTransform rollingScreen;
    [SerializeField] private RectTransform gameOverScreen;

    private void Awake()
    {
        startScreen.WrapClicked += OnWrapClicked;
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
    }
}