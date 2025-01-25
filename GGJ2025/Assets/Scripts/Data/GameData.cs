using UnityEngine;

[CreateAssetMenu(fileName = "Game", menuName = "Data/Game", order = 0)]
public class GameData : ScriptableObject
{
    [SerializeField] private int maxLaunchAngle = 45;
    public int MaxLaunchAngle => maxLaunchAngle;

    [SerializeField] private int maxLaunchImpulse = 10;
    public int MaxLaunchImpulse => maxLaunchImpulse;
    
    [SerializeField] private int bubbleWrapAmount;
    public int BubbleWrapAmount => bubbleWrapAmount;
    
    [SerializeField] private ThrowableData[] throwables;
}