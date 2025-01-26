using System;
using UnityEngine;

public class WrapManager : MonoBehaviour
{
    public Action<Throwable> StartClicked;
    
    [SerializeField] private WrapScreen wrapScreen;
    [Range(0.1f, 1f)][Tooltip("In %")]
    [SerializeField] private float maxWrapOpacity = 0.25f;
    
    private Throwable[] throwables;
    private Throwable currentThrowable;
    private int currentThrowableIndex;

    private int maxWrapAmount;
    private int totalAssignedWrap;
    private int RemainingWrap => maxWrapAmount - totalAssignedWrap;
    
    public void Initialise(GameData gameData)
    {
        maxWrapAmount = gameData.BubbleWrapAmount;
        totalAssignedWrap = 0;
        
        wrapScreen.SelectionChanged -= OnSelectionChanged;
        wrapScreen.AmountChanged -= OnAmountChanged;
        wrapScreen.StartClicked -= OnStartClicked;
        
        wrapScreen.SelectionChanged += OnSelectionChanged;
        wrapScreen.AmountChanged += OnAmountChanged;
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
            instance.Initialise(gameData.SidewaysMoveImpulse, data.CuteName, data.Health);
            instance.SetAssignedWrap(0, maxWrapAmount, maxWrapOpacity);
            throwables[index] = instance;
            instance.gameObject.SetActive(false);
        }
        
        SelectThrowable(0);
    }

    private void SelectThrowable(int index)
    {
        if(index < 0) index = throwables.Length - 1;
        else if (index >= throwables.Length) index = 0;
        
        if(currentThrowable) currentThrowable.gameObject.SetActive(false);
        currentThrowableIndex = index;
        currentThrowable = throwables[index];   
        currentThrowable.gameObject.SetActive(true);
        SetUI();
    }

    private void SetUI()
    {
        wrapScreen.Set(currentThrowable.Name, currentThrowable.HealthPoints, currentThrowable.AssignedWrap, RemainingWrap );
    }

    private void OnSelectionChanged(int delta)
    {
        SelectThrowable(currentThrowableIndex + delta);
    }
    
    private void OnAmountChanged(int delta)
    {
        currentThrowable.SetAssignedWrap(currentThrowable.AssignedWrap + delta, maxWrapAmount, maxWrapOpacity);
        totalAssignedWrap += delta;
        SetUI();
    }

    private void OnStartClicked()
    {
        StartClicked?.Invoke(currentThrowable);
    }
}