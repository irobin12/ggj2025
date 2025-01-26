using System;
using System.Collections.Generic;
using UnityEngine;

public class WrapManager : MonoBehaviour
{
    public Action<Throwable> StartClicked;
    
    [SerializeField] private WrapScreen wrapScreen;
    [Range(0.1f, 1f)][Tooltip("In %")]
    [SerializeField] private float maxWrapOpacity = 0.25f;
    
    public Throwable[] Throwables { get; private set; }
    public Throwable CurrentThrowable { get; private set; }
    public int CurrentThrowableIndex { get; private set; }

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
        Throwables = new Throwable[throwablesData.Length];

        for (int i = transform.childCount - 1; i >= 0; i--)
        {
            var child = transform.GetChild(i);
            Destroy(child.gameObject);
        }

        for (var index = 0; index < throwablesData.Length; index++)
        {
            var data = throwablesData[index];
            var instance = Instantiate(data.Prefab, transform);
            instance.Initialise(gameData.SidewaysMoveImpulse, data);
            instance.SetAssignedWrap(0, maxWrapAmount, maxWrapOpacity);
            Throwables[index] = instance;
            instance.gameObject.SetActive(false);
        }
        
        SelectThrowable(0);
    }

    private void SelectThrowable(int index)
    {
        if(index < 0) index = Throwables.Length - 1;
        else if (index >= Throwables.Length) index = 0;
        
        if(CurrentThrowable) CurrentThrowable.gameObject.SetActive(false);
        CurrentThrowableIndex = index;
        CurrentThrowable = Throwables[index];   
        CurrentThrowable.gameObject.SetActive(true);
        SetUI();
    }

    private void SetUI()
    {
        wrapScreen.Set(CurrentThrowable.Name, CurrentThrowable.MaxHealthPoints, CurrentThrowable.AssignedWrap, RemainingWrap );
    }

    private void OnSelectionChanged(int delta)
    {
        SelectThrowable(CurrentThrowableIndex + delta);
    }
    
    private void OnAmountChanged(int delta)
    {
        CurrentThrowable.SetAssignedWrap(CurrentThrowable.AssignedWrap + delta, maxWrapAmount, maxWrapOpacity);
        totalAssignedWrap += delta;
        SetUI();
    }

    private void OnStartClicked()
    {
        GameManager.ThrowableQueue = new Queue<Throwable>(Throwables);

        // bool firstItemDone = false;
        // for (int i = CurrentThrowableIndex; i < Throwables.Length; i++)
        // {
        //     
        //     var t = Throwables[i];
        //     ThrowableQueue.Enqueue(t);
        //     firstItemDone = true;
        //
        // }

        int iteration = 0;
        int index = CurrentThrowableIndex;

        while (iteration < Throwables.Length)
        {
            if (index >= Throwables.Length)
            {
                index = 0;
            }
            var t = Throwables[index];
            GameManager.ThrowableQueue.Enqueue(t);
            iteration++;
            index++;
        }
        
        StartClicked?.Invoke(CurrentThrowable);
    }
}