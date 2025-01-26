using UnityEngine;

[CreateAssetMenu(fileName = "Input", menuName = "Data/Input", order = 1)]
public class InputData : ScriptableObject
{
    [SerializeField] private KeyCode[] moveLeft;
    public KeyCode[] MoveLeft => moveLeft;
    
    [SerializeField] private KeyCode[] moveRight;
    public KeyCode[] MoveRight => moveRight;

    [SerializeField] private KeyCode[] up;
    public KeyCode[] Up => up;
    
    [SerializeField] private KeyCode[] down;
    public KeyCode[] Down => down;

    [SerializeField] private KeyCode[] restart;
    public KeyCode[] Restart => restart;
    
    [SerializeField] private KeyCode[] menu;
    public KeyCode[] Menu => menu;
}