using UnityEngine;

[CreateAssetMenu(fileName = "Game", menuName = "Data/Game", order = 0)]
public class GameData : ScriptableObject
{
    [Range(0, 90)]
    [SerializeField] private int maxLaunchAngle = 45;
    public int MaxLaunchAngle => maxLaunchAngle;

    [Range(0, 1f)]
    // [Tooltip("0 = edge of the screen is max angle, 0.5 = middle of the screen is max angle (not possible to change it)")]
    [Tooltip("0 = no width considered, angle cannot be changed, 1 = full screen width is considered")]
    [SerializeField] private float screenProportionForLaunchAngle = 0.8f;
    public float ScreenProportionForLaunchAngle => screenProportionForLaunchAngle;
    
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