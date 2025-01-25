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
    private Vector3 cameraStartPosition;
    private Quaternion cameraInitialRotation;
    
    private Vector3 throwableInitialPosition;
    private Quaternion throwableInitialRotation;
    
    private float minLaunchAngle;
    private float maxLaunchAngle;
    // private float minScreenMarginForLaunchAngle;
    // private float maxScreenMarginForLaunchAngle;
    // private float screenProportionForLaunchAngle;
    private float screenWidthToConsider;
    private float mouseOffset;
    
    
    private int maxLaunchImpulse;

    public void Initialize(int dataMaxLaunchAngle, int dataMaxLaunchImpulse, float dataScreenProportionForLaunchAngle)
    {
        initialPosition = transform.position;
        initialRotation = transform.rotation;
        
        cameraInitialPosition = followerCamera.transform.position;
        cameraInitialRotation = followerCamera.transform.rotation;
        
        throwableInitialPosition = throwable.transform.position;
        throwableInitialRotation = throwable.transform.rotation;
        
        minLaunchAngle = throwableInitialRotation.eulerAngles.y - dataMaxLaunchAngle;
        maxLaunchAngle = throwableInitialRotation.eulerAngles.y + dataMaxLaunchAngle;
        // minScreenMarginForLaunchAngle = 0 + dataScreenProportionForLaunchAngle;
        // maxScreenMarginForLaunchAngle = 1 - dataScreenProportionForLaunchAngle;
        // screenProportionForLaunchAngle = dataScreenProportionForLaunchAngle;
        screenWidthToConsider = Screen.width * dataScreenProportionForLaunchAngle;
        mouseOffset = (Screen.width - screenWidthToConsider) / 2;
        
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
        cameraStartPosition = followerCamera.transform.position;
        throwable.Launch(maxLaunchImpulse);
        ShowLauncherVisuals(false);
        GameStatesManager.SetGameState(GameStatesManager.GameState.Rolling);
    }

    private void UpdateLauncherRotation()
    {
        var mousePosition = Input.mousePosition;
        // var mouseX = Mathf.Clamp(mousePosition.x, mouseOffset, Screen.width - mouseOffset);
        // var newAngle = Mathf.LerpAngle(minLaunchAngle, maxLaunchAngle, mouseX / screenWidthToConsider);
        var newAngle = Mathf.LerpAngle(minLaunchAngle, maxLaunchAngle, mousePosition.x / Screen.width);
        // followerCamera.transform.rotation = new Vector3(cameraInitialRotation.x, newAngle, cameraInitialRotation.z);
        var newRotation = Quaternion.Euler(0, newAngle, 0);
        transform.rotation = newRotation;
        // var newRotation = Quaternion.Euler(cameraInitialRotation.eulerAngles.x, newAngle, cameraInitialRotation.eulerAngles.z);
        // followerCamera.transform.rotation = newRotation;
        // launcherVisual.transform.rotation = newRotation;
        // throwable.transform.rotation = newRotation;
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
        Vector3 positionOffset = throwable.transform.position - throwableInitialPosition;
        followerCamera.transform.position = cameraStartPosition + positionOffset;
        
        // var direction = throwable.rigidBody.
        // var velocity = throwable.rigidBody.GetPointVelocity(Vector3.zero);
        // Debug.Log(velocity);
        // var facing = throwable.rigidBody.rotation * Vector3.forward;
        var velocity = throwable.rigidBody.linearVelocity;
        // float angleDifference = Vector3.Angle(facing, velocity);
        // float relativeAngleDifference = Vector3.SignedAngle(facing, velocity, Vector3.up);
        // followerCamera.transform.rotation = Quaternion.AngleAxis(10f, Vector3.up);
        var currentYRotation = GetCameraYRotation();
        followerCamera.transform.rotation = Quaternion.LookRotation(velocity, Vector3.up);
        var newYRotation = GetCameraYRotation();
        if (!Mathf.Approximately(newYRotation, currentYRotation))
        {
            // StartCoroutine(RotateCamera(currentYRotation, newYRotation));
            followerCamera.transform.rotation = Quaternion.Euler(new Vector3(cameraInitialRotation.eulerAngles.x, newYRotation, cameraInitialRotation.eulerAngles.z));
        }
    }

    private IEnumerator RotateCamera(float originYRotation, float targetYRotation)
    {
        float currentYRotation = originYRotation;
        while (!Mathf.Approximately(currentYRotation, targetYRotation))
        {
            currentYRotation += Time.deltaTime * 10f;
            followerCamera.transform.rotation = Quaternion.Euler(0, currentYRotation, 0);
            yield return null;
        }
    }

    private float GetCameraYRotation()
    {
        return followerCamera.transform.rotation.eulerAngles.y;
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