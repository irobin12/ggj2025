using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class Throwable : MonoBehaviour
{
    private Rigidbody rigidBody;

    private void Awake()
    {
        rigidBody = GetComponent<Rigidbody>();
        SetGravity(false);
    }

    public void Launch(float impulse)   
    {
        rigidBody.AddRelativeForce(new Vector3(0, 0, impulse), ForceMode.Impulse);
        SetGravity(true);
    }

    private void SetGravity(bool useGravity)
    {
        rigidBody.useGravity = useGravity;
    }

    public void Restart(Vector3 position, Quaternion rotation)
    {
        transform.position = position;
        transform.rotation = rotation;
        rigidBody.linearVelocity = Vector3.zero;
        rigidBody.angularVelocity = Vector3.zero;
        SetGravity(false);
    }
}
