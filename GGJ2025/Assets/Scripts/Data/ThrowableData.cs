using UnityEngine;

[CreateAssetMenu(fileName = "Throwable", menuName = "Data/Throwable", order = 1)]
public class ThrowableData : ScriptableObject
{
    [SerializeField] private int health;
    public int Health => health;
    
    [SerializeField] private Throwable prefab;
    public Throwable Prefab => prefab;
}