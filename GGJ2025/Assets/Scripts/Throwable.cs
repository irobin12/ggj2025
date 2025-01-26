using System;
using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class Throwable : MonoBehaviour
{
    public Action TookDamage;
    public Action Died;
    public Action Finished;
    
    private const string ObstacleTag = "Obstacle";
    private const string FinishLineTag = "FinishLine";
    public string Name => throwableData.CuteName;
    public int MaxHealthPoints => throwableData.Health;
    public Sprite Icon => throwableData.Icon;
    public int AssignedWrap {get; private set;}

    [SerializeField] private Renderer wrap;
    
    private Rigidbody rigidBody;
    private Material wrapMaterial;
    private int turnImpulse;
    private bool moveLeft;
    private bool moveRight;
    
    private ThrowableData throwableData;

    private bool stayingInCollision;
    private int currentCollisionsCount = 0;

    public int CurrentHealth { get; private set; }
    public int CurrentWrap {get; private set;}

    private void Awake()
    {
        wrapMaterial = wrap.material;
        rigidBody = GetComponent<Rigidbody>();
    }

    private void OnCollisionEnter(Collision other)
    {
        if (other.gameObject.CompareTag(ObstacleTag))
        {
            Debug.Log("Collision enter");
            currentCollisionsCount++;

            if (!stayingInCollision)
            {
                TakeDamage();
            }

            stayingInCollision = true;
        } 
        
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag(FinishLineTag))
        {
            Debug.Log("Finished!");
            Finished?.Invoke();
        }
    }

    private void TakeDamage()
    {
        if(CurrentHealth == 0) return;
        
        if (CurrentWrap == 0)
        {
            Debug.Log("Lose 1HP!");
            CurrentHealth--;
        }
        else
        {
            Debug.Log("Lose 1 Wrap!");
            CurrentWrap--;
        }
        
        TookDamage?.Invoke();
        
        if (CurrentHealth == 0)
        {
            Debug.Log("You DIED");
            Died?.Invoke();
        }
    }

    private void OnCollisionExit(Collision other)
    {
        if (other.gameObject.CompareTag(ObstacleTag))
        {
            currentCollisionsCount--;
            if (currentCollisionsCount == 0)
            {
                stayingInCollision = false;
            }
        }
    }

    public void Initialise(int sidewaysMoveImpulse, ThrowableData data)
    {
        SetGravity(false);
        
        turnImpulse = sidewaysMoveImpulse;
        throwableData = data;
        
        CurrentHealth = MaxHealthPoints;
        CurrentWrap = AssignedWrap;
    }

    public void SetAssignedWrap(int amount, int maxPotentialAmount, float maxWrapOpacity)
    {
        AssignedWrap = amount;
        CurrentWrap = amount;
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
        if(GameStatesManager.CurrentGameState != GameStatesManager.States.Rolling) return;
        
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
