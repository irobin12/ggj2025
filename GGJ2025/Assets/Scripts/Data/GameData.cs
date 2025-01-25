using UnityEngine;

[CreateAssetMenu(fileName = "Game", menuName = "Data/Game", order = 0)]
public class GameData : ScriptableObject
{
    [Range(0, 45)]
    [SerializeField] private int maxLaunchAngle = 45;
    public int MaxLaunchAngle => maxLaunchAngle;

    [Range(10, 100)]
    [SerializeField] private int maxLaunchImpulse = 50;
    public int MaxLaunchImpulse => maxLaunchImpulse;
    
    [SerializeField] private int bubbleWrapAmount;
    public int BubbleWrapAmount => bubbleWrapAmount;
    
    [SerializeField] private ThrowableData[] throwables;
}