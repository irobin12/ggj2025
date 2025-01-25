using UnityEngine;

public class GameManager : MonoBehaviour
{
    [SerializeField] private GameData gameData;
    [SerializeField] private ThrowableManager throwableManager;
    
    private void Awake()
    {
        throwableManager.Initialize(gameData.MaxLaunchAngle, gameData.MaxLaunchImpulse, gameData.ScreenProportionForLaunchAngle);
    }

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
