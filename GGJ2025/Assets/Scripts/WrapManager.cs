using System;
using UnityEngine;

public class WrapManager : MonoBehaviour
{
    public Action<Throwable> StartClicked;
    [SerializeField] private WrapScreen wrapScreen;
    private Throwable[] throwables;
    private Throwable currentThrowable;
    
    public void Initialise(GameData gameData)
    {
        wrapScreen.StartClicked += OnStartClicked;
        ThrowableData[] throwablesData = gameData.Throwables;
        throwables = new Throwable[throwablesData.Length];

        for (int i = transform.childCount - 1; i >= 0; i--)
        {
            var child = transform.GetChild(i);
            Destroy(child.gameObject);
        }

        for (var index = 0; index < throwablesData.Length; index++)
        {
            var data = throwablesData[index];
            var instance = Instantiate(data.Prefab, transform);
            instance.Initialise(gameData.SidewaysMoveImpulse, data.name, data.Health);
            throwables[index] = instance;
            instance.gameObject.SetActive(false);
        }
        
        SelectThrowable(0);
    }

    private void SelectThrowable(int index)
    {
        currentThrowable = throwables[index];   
        currentThrowable.gameObject.SetActive(true);
    }
    
    private void OnStartClicked()
    {
        StartClicked?.Invoke(currentThrowable);
    }
}