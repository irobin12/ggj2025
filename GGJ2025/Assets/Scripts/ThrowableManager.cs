using System.Collections;
using UnityEngine;

public class ThrowableManager : MonoBehaviour
{
    [SerializeField] private Camera followerCamera;
    [SerializeField] private Throwable throwable;
    [SerializeField] private GameObject launcherVisual;
    [SerializeField] private Color minImpulseColor = Color.yellow;
    [SerializeField] private Color maxImpulseColor = Color.red;
    
    private Vector3 initialPosition;
    private Quaternion initialRotation;
    
    private Vector3 cameraInitialPosition;
    private Quaternion cameraInitialRotation;
    private Vector3 cameraDeltaPositionFromThrowable;
    
    private Vector3 throwableInitialPosition;
    private Quaternion throwableInitialRotation;
    
    private float minLaunchAngle;
    private float maxLaunchAngle;
    
    private int maxLaunchImpulse;

    public void Initialize(int dataMaxLaunchAngle, int dataMaxLaunchImpulse, float dataScreenProportionForLaunchAngle)
    {
        initialPosition = transform.position;
        initialRotation = transform.rotation;
        
        cameraInitialPosition = followerCamera.transform.position;
        cameraInitialRotation = followerCamera.transform.rotation;
        
        throwableInitialPosition = throwable.transform.position;
        throwableInitialRotation = throwable.transform.rotation;

        cameraDeltaPositionFromThrowable = throwableInitialPosition - cameraInitialPosition;
        
        minLaunchAngle = throwableInitialRotation.eulerAngles.y - dataMaxLaunchAngle;
        maxLaunchAngle = throwableInitialRotation.eulerAngles.y + dataMaxLaunchAngle;
        
        maxLaunchImpulse = dataMaxLaunchImpulse;
        
        ShowLauncherVisuals(true);
    }

    private void Restart()
    {
        StopAllCoroutines();
        transform.position = initialPosition;
        transform.rotation = initialRotation;
        
        followerCamera.transform.position = cameraInitialPosition;
        followerCamera.transform.rotation = cameraInitialRotation;
        
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
                UpdateLaunchingRotation();
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

    private void UpdateLaunchingRotation()
    {
        var mousePosition = Input.mousePosition;
        var newAngle = Mathf.LerpAngle(minLaunchAngle, maxLaunchAngle, mousePosition.x / Screen.width);
        launcherVisual.transform.rotation = Quaternion.Euler(launcherVisual.transform.rotation.eulerAngles.x, newAngle, 0);
        throwable.transform.rotation = Quaternion.Euler(throwable.transform.rotation.eulerAngles.x, newAngle, throwable.transform.rotation.eulerAngles.z);
    }

    private void FixedUpdate()
    {
        if (GameStatesManager.CurrentGameState == GameStatesManager.GameState.Rolling)
        {
            UpdateCameraTransformFromThrowable();
        }
    }

    private void UpdateCameraTransformFromThrowable()
    {
        // x should stay fixed (width), z is length.depth, y is height
        followerCamera.transform.position = new Vector3(followerCamera.transform.position.x, throwable.transform.position.y - cameraDeltaPositionFromThrowable.y, throwable.transform.position.z - cameraDeltaPositionFromThrowable.z);
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