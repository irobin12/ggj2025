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
    private Vector2 screenMiddle;

    public void Initialize(int gameDataMaxLaunchAngle, int gameDataMaxLaunchImpulse)
    {
        cameraInitialPosition = followerCamera.transform.position;
        throwableInitialPosition = throwable.transform.position;
        throwableInitialRotation = throwable.transform.rotation;
        
        minLaunchAngle = throwableInitialRotation.eulerAngles.y - gameDataMaxLaunchAngle;
        maxLaunchAngle = throwableInitialRotation.eulerAngles.y + gameDataMaxLaunchAngle;
        
        maxLaunchImpulse = gameDataMaxLaunchImpulse;
        screenMiddle = new Vector2(Screen.width / 2f, Screen.height / 2f);
    }

    private void Update()
    {
        if (GameStatesManager.CurrentGameState == GameStatesManager.GameState.Launch)
        {
            UpdateLauncher();
        }
    }

    private void UpdateLauncher()
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
        followerCamera.transform.position = cameraInitialPosition;
        Vector3 positionOffset = throwable.transform.position - throwableInitialPosition;
        followerCamera.transform.position = cameraInitialPosition + positionOffset;
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