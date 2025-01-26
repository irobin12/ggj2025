using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class Throwable : MonoBehaviour
{
    private Rigidbody rigidBody;
    private int turnImpulse;
    private bool moveLeft;
    private bool moveRight;

    private string throwableNme;
    private int healthPoints;

    public void Initialise(int sidewaysMoveImpulse, string throwableName, int healthPoints)
    {
        rigidBody = GetComponent<Rigidbody>();
        SetGravity(false);
        
        turnImpulse = sidewaysMoveImpulse;
        this.throwableNme = throwableName;
        this.healthPoints = healthPoints;
    }

    private void Update()
    {
        CheckMovementInput();
    }

    private void CheckMovementInput()
    {
        if (Inputs.IsKeyDown(Inputs.MoveLeft))
        {
            moveLeft = true;
            return;
        }

        if (Inputs.IsKeyUp(Inputs.MoveLeft))
        {
            moveLeft = false;
            return;
        }
        
        if (Inputs.IsKeyDown(Inputs.MoveRight))
        {
            moveRight = true;
            return;
        }

        if (Inputs.IsKeyUp(Inputs.MoveRight))
        {
            moveRight = false;
            return;
        }
    }

    private void FixedUpdate()
    {
        MoveSideways();
    }

    private void MoveSideways()
    {
        if (moveLeft)
        {
            rigidBody.AddForce(Vector3.left * turnImpulse , ForceMode.Impulse);
        } 
        else if (moveRight)
        {
            rigidBody.AddForce(Vector3.right * turnImpulse , ForceMode.Impulse);
        }
    }

    public void Launch(float launchImpulse)   
    {
        rigidBody.AddRelativeForce(new Vector3(0, 0, launchImpulse), ForceMode.Impulse);
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
