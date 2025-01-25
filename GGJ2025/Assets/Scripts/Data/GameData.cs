using UnityEngine;

[CreateAssetMenu(fileName = "Game", menuName = "Data/Game", order = 0)]
public class GameData : ScriptableObject
{
    [SerializeField] private InputData inputData;
    public InputData InputData => inputData;
    
    [Range(0, 90)]
    [SerializeField] private int maxLaunchAngle = 45;
    public int MaxLaunchAngle => maxLaunchAngle;
    
    [Range(10, 500)]
    [SerializeField] private int maxLaunchImpulse = 50;
    public int MaxLaunchImpulse => maxLaunchImpulse;

    [Range(1, 10)]
    [SerializeField] private int sidewaysMoveImpulse = 5;
    public int SidewaysMoveImpulse => sidewaysMoveImpulse;
    
    [SerializeField] private int bubbleWrapAmount;
    public int BubbleWrapAmount => bubbleWrapAmount;
    
    [SerializeField] private ThrowableData[] throwables;
}