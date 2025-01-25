using UnityEngine;

public class GameManager : MonoBehaviour
{
    [SerializeField] private GameData gameData;
    [SerializeField] private ThrowableManager throwableManager;
    
    private void Awake()
    {
        throwableManager.Initialise(gameData);
        Inputs.Set(gameData.InputData);
    }

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        switch (GameStatesManager.CurrentGameState)
        {
            case GameStatesManager.GameState.Launch:
            {
                UpdateLaunch();

                break;
            }

            case GameStatesManager.GameState.Rolling:
            case GameStatesManager.GameState.GameOver:
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
