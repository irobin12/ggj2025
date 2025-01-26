using System;
using UnityEngine;

public class ThrowableManager : MonoBehaviour
{
    public Action<Throwable> ThrowableDamaged;
    public Action<Throwable> ThrowableDied;
    
    [SerializeField] private Camera followerCamera;
    [SerializeField] private Transform spawnPoint;
    [SerializeField] private GameObject launcherVisual;
    [SerializeField] private Color minImpulseColor = Color.yellow;
    [SerializeField] private Color maxImpulseColor = Color.red;
    
    public Throwable CurrentThrowable { get; private set; }
    
    private Vector3 cameraInitialPosition;
    private Quaternion cameraInitialRotation;
    private Vector3 cameraDeltaPositionFromThrowable;
    
    private float minLaunchAngle;
    private float maxLaunchAngle;

    private int minLaunchImpulse;
    private int maxLaunchImpulse;
    private float launchImpulse;
    
    private float maxImpulseHeldDownTime;
    private float impulseHeldDownTime;

    public void Initialise(GameData data, Throwable selectedThrowable)
    {
        CurrentThrowable = selectedThrowable;
        CurrentThrowable.TookDamage -= OnThrowableDamaged;
        CurrentThrowable.Died -= OnThrowableDied;
        CurrentThrowable.TookDamage += OnThrowableDamaged;
        CurrentThrowable.Died += OnThrowableDied;
        
        cameraInitialPosition = followerCamera.transform.position;
        cameraInitialRotation = followerCamera.transform.rotation;
        
        CurrentThrowable.transform.position = spawnPoint.position;
        CurrentThrowable.transform.rotation = spawnPoint.rotation;

        cameraDeltaPositionFromThrowable = spawnPoint.position - cameraInitialPosition;
        
        minLaunchAngle = spawnPoint.rotation.eulerAngles.y - data.MaxLaunchAngle;
        maxLaunchAngle = spawnPoint.rotation.eulerAngles.y + data.MaxLaunchAngle;
        
        minLaunchImpulse = data.MinLaunchImpulse;
        maxLaunchImpulse = data.MaxLaunchImpulse;
        maxImpulseHeldDownTime = data.MaxImpulseHeldDownTime;
        
        SetStartingValues();

        ShowLauncherVisuals(true);
    }

    private void OnThrowableDied()
    {
        ThrowableDied?.Invoke(CurrentThrowable);
        CurrentThrowable.gameObject.SetActive(false);
        GameStatesManager.SetGameState(GameStatesManager.States.GameOver);
    }

    private void OnThrowableDamaged()
    {
        // Update UI 
        ThrowableDamaged?.Invoke(CurrentThrowable);
    }

    public void ShowThrower(bool show)
    {
        launcherVisual.SetActive(show);
        if(CurrentThrowable) CurrentThrowable.gameObject.SetActive(show);
    }
    
    private void SetStartingValues()
    {
        launchImpulse = minLaunchImpulse;
        impulseHeldDownTime = 0f;
        ColorVisuals(0);
    }

    public void Restart()
    {
        StopAllCoroutines();
        
        followerCamera.transform.position = cameraInitialPosition;
        followerCamera.transform.rotation = cameraInitialRotation;
        
        SetStartingValues();

        CurrentThrowable.Restart(spawnPoint.position, spawnPoint.rotation);
        ShowLauncherVisuals(true);
        // GameStatesManager.CurrentGameState = GameStatesManager.States.Launch;
    }

    public void UpdateLaunch()
    {
        UpdateLaunchingRotation();
        
        if (Input.GetMouseButton(0))
        {
            impulseHeldDownTime += Time.deltaTime;
            
            if (launchImpulse < maxLaunchImpulse)
            {
                var lerpValue = Mathf.InverseLerp(0, maxImpulseHeldDownTime, impulseHeldDownTime);
                launchImpulse = Mathf.Lerp(minLaunchImpulse, maxLaunchImpulse, lerpValue);
                ColorVisuals(lerpValue);
            }
        }
        
        if (Input.GetMouseButtonUp(0))
        {
            LaunchThrowable();
        }
    }

    private void LaunchThrowable()
    {
        CurrentThrowable.Launch(launchImpulse);
        ShowLauncherVisuals(false);
        GameStatesManager.SetGameState(GameStatesManager.States.Rolling);
    }

    private void UpdateLaunchingRotation()
    {
        var mousePosition = Input.mousePosition;
        var newAngle = Mathf.LerpAngle(minLaunchAngle, maxLaunchAngle, mousePosition.x / Screen.width);
        launcherVisual.transform.rotation = Quaternion.Euler(launcherVisual.transform.rotation.eulerAngles.x, newAngle, 0);
        CurrentThrowable.transform.rotation = Quaternion.Euler(CurrentThrowable.transform.rotation.eulerAngles.x, newAngle, CurrentThrowable.transform.rotation.eulerAngles.z);
    }

    private void FixedUpdate()
    {
        if (GameStatesManager.CurrentGameState == GameStatesManager.States.Rolling)
        {
            UpdateCameraTransformFromThrowable();
        }
    }

    private void UpdateCameraTransformFromThrowable()
    {
        // x should stay fixed (width), z is length.depth, y is height
        followerCamera.transform.position = new Vector3(followerCamera.transform.position.x, CurrentThrowable.transform.position.y - cameraDeltaPositionFromThrowable.y, CurrentThrowable.transform.position.z - cameraDeltaPositionFromThrowable.z);
    }

    private void ShowLauncherVisuals(bool show)
    {
        launcherVisual.SetActive(show);
    }
    
    private void ColorVisuals(float lerpValue)
    {
        //TODO cleaner when proper visuals
        var sprites = launcherVisual.GetComponentsInChildren<SpriteRenderer>();
        
        
        
        var color = Color.Lerp(minImpulseColor, maxImpulseColor, lerpValue);
        
        foreach (var sprite in sprites)
        {
            sprite.color = color;
        }
    }
}