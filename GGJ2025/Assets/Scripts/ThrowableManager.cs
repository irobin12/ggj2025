using UnityEngine;

public class ThrowableManager : MonoBehaviour
{
    [SerializeField] private Camera followerCamera;
    [SerializeField] private Throwable throwable;
    [SerializeField] private GameObject launcherVisual;
    [SerializeField] private Color minImpulseColor = Color.yellow;
    [SerializeField] private Color maxImpulseColor = Color.red;
    
    private Vector3 cameraInitialPosition;
    private Vector3 throwableInitialPosition;
    private Quaternion throwableInitialRotation;
    private float minLaunchAngle;
    private float maxLaunchAngle;
    private int maxLaunchImpulse;
    private Vector3 initialPosition;
    private Quaternion initialRotation;

    public void Initialize(int gameDataMaxLaunchAngle, int gameDataMaxLaunchImpulse)
    {
        initialPosition = transform.position;
        initialRotation = transform.rotation;
        // cameraInitialPosition = followerCamera.transform.position;
        throwableInitialPosition = throwable.transform.position;
        throwableInitialRotation = throwable.transform.rotation;
        
        minLaunchAngle = throwableInitialRotation.eulerAngles.y - gameDataMaxLaunchAngle;
        maxLaunchAngle = throwableInitialRotation.eulerAngles.y + gameDataMaxLaunchAngle;
        
        maxLaunchImpulse = gameDataMaxLaunchImpulse;
        
        ShowLauncherVisuals(true);
    }

    private void Restart()
    {
        transform.position = initialPosition;
        transform.rotation = initialRotation;
        throwable.Restart(throwableInitialPosition, throwableInitialRotation);
        ShowLauncherVisuals(true);
        GameStatesManager.CurrentGameState = GameStatesManager.GameState.Launch;
    }

    private void Update()
    {
        switch (GameStatesManager.CurrentGameState)
        {
            case GameStatesManager.GameState.Launch:
            {
                UpdateLauncherRotation();
                if (Input.GetMouseButtonDown(0))
                {
                    LaunchThrowable();
                }

                break;
            }

            case GameStatesManager.GameState.Rolling:
            case GameStatesManager.GameState.GameOver:
            {
                if (Input.GetKeyUp(KeyCode.R))
                {
                    Restart();
                }
                
                break;
            }
        }
    }

    private void LaunchThrowable()
    {
        throwable.Launch(maxLaunchImpulse);
        ShowLauncherVisuals(false);
        GameStatesManager.SetGameState(GameStatesManager.GameState.Rolling);
    }

    private void UpdateLauncherRotation()
    {
        var mousePosition = Input.mousePosition;
        var newAngle = Mathf.LerpAngle(minLaunchAngle, maxLaunchAngle, mousePosition.x / Screen.width);
        transform.rotation = Quaternion.Euler(new Vector3(transform.rotation.x, newAngle, transform.rotation.z));
    }

    private void FixedUpdate()
    {
        if (GameStatesManager.CurrentGameState == GameStatesManager.GameState.Rolling)
        {
            UpdateCameraPositionFromThrowable();
        }
    }

    private void UpdateCameraPositionFromThrowable()
    {
        transform.position = initialPosition;
        Vector3 positionOffset = throwable.transform.position - throwableInitialPosition;
        transform.position = initialPosition + positionOffset;
    }

    private void ShowLauncherVisuals(bool show)
    {
        launcherVisual.SetActive(show);
    }
    
    private void ColorVisuals(Color color)
    {
        //TODO cleaner when proper visuals
        var sprites = launcherVisual.GetComponentsInChildren<SpriteRenderer>();
        foreach (var sprite in sprites)
        {
            sprite.color = color;
        }
    }
}