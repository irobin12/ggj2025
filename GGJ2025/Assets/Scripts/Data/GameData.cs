using UnityEngine;

[CreateAssetMenu(fileName = "Game", menuName = "Data/Game", order = 0)]
public class GameData : ScriptableObject
{
    [SerializeField] private InputData inputData;
    public InputData InputData => inputData;
    
    [Header("Launch")]
    [Range(0, 90)]
    [SerializeField] private int maxLaunchAngle = 45;
    public int MaxLaunchAngle => maxLaunchAngle;
    
    [Range(1, 499)]
    [SerializeField] private int minLaunchImpulse = 10;
    public int MinLaunchImpulse => minLaunchImpulse;
    
    [Range(2, 500)]
    [SerializeField] private int maxLaunchImpulse = 50;
    public int MaxLaunchImpulse => maxLaunchImpulse;

    [Range(0, 20)] [Tooltip("In seconds. Time over which the launch impulse will increase until reaching max value. 0 means the launch impulse will always be the min value.")]
    [SerializeField] private float maxImpulseHeldDownTime = 2f;
    public float MaxImpulseHeldDownTime => maxImpulseHeldDownTime;

    [Space]
    [Range(1, 10)]
    [SerializeField] private int sidewaysMoveImpulse = 5;
    public int SidewaysMoveImpulse => sidewaysMoveImpulse;
    
    [SerializeField] private int bubbleWrapAmount;
    public int BubbleWrapAmount => bubbleWrapAmount;
    
    [SerializeField] private ThrowableData[] throwables;
}