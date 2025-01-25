using UnityEngine;

[CreateAssetMenu(fileName = "Item", menuName = "Data/Item", order = 1)]
public class ThrowableData : ScriptableObject
{
    [SerializeField] private int health;
    public int Health => health;
    
    [SerializeField] private Throwable prefab;
    public Throwable Prefab => prefab;
}