using UnityEngine;

[CreateAssetMenu(fileName = "Game", menuName = "Data/Game", order = 0)]
public class GameData : ScriptableObject
{
    [SerializeField] private int bubbleWrapAmount;
    public int BubbleWrapAmount => bubbleWrapAmount;
    
    [SerializeField] private ThrowableData[] throwables;
}