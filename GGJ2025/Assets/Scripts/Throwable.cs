using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class Throwable : MonoBehaviour
{
    public string Name => throwableData.CuteName;
    public int HealthPoints => throwableData.Health;
    public int AssignedWrap {get; private set;}

    [SerializeField] private Renderer wrap;
    
    private Rigidbody rigidBody;
    private Material wrapMaterial;
    private int turnImpulse;
    private bool moveLeft;
    private bool moveRight;
    
    private ThrowableData throwableData;

    private void Awake()
    {
        wrapMaterial = wrap.material;
    }

    public void Initialise(int sidewaysMoveImpulse, ThrowableData data)
    {
        rigidBody = GetComponent<Rigidbody>();
        SetGravity(false);
        
        turnImpulse = sidewaysMoveImpulse;
        throwableData = data;
    }

    public void SetAssignedWrap(int amount, int maxPotentialAmount, float maxWrapOpacity)
    {
        AssignedWrap = amount;
        Color color = wrapMaterial.color;
        var lerpValue = Mathf.InverseLerp(0, maxPotentialAmount, AssignedWrap);
        color.a = Mathf.Lerp(0, maxWrapOpacity, lerpValue);
        wrap.material.color = color;
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
