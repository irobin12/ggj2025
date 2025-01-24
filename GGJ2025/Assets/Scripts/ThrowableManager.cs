using UnityEngine;

public class ThrowableManager : MonoBehaviour
{
    [SerializeField] private Camera camera;
    [SerializeField] private Throwable throwable;
    
    private Vector3 cameraInitialPosition;
    private Vector3 throwableInitialPosition;

    private void Awake()
    {
        cameraInitialPosition = camera.transform.position;
        throwableInitialPosition = throwable.transform.position;
    }

    private void FixedUpdate()
    {
        UpdateCameraPosition();
    }

    private void UpdateCameraPosition()
    {
        camera.transform.position = cameraInitialPosition;
        Vector3 positionOffset = throwable.transform.position - throwableInitialPosition;
        camera.transform.position = cameraInitialPosition + positionOffset;
    }
}