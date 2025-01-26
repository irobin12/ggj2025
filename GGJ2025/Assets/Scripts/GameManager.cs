using UnityEngine;

public class GameManager : MonoBehaviour
{
    private const int DefaultLevelIndex = 0;
    [SerializeField] private GameData gameData;
    [SerializeField] private ThrowableManager throwableManager;
    [SerializeField] private UIManager uiManager;
    
    private void Awake()
    {
        throwableManager.Initialise(gameData);
        Inputs.Set(gameData.InputData);
        LevelsManager.SetUp(gameData.LevelNames);
        LevelsManager.LoadLevelAdditive(DefaultLevelIndex);
    }
    
    void Update()
    {
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
        if (Inputs.IsKeyUp(Inputs.restart))
        {
            throwableManager.Restart();
        }
    }
}
