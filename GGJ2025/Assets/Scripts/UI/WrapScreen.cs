using System;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class WrapScreen : MonoBehaviour
{
    public Action StartClicked;
    public Action<int> SelectionChanged;
    public Action<int> AmountChanged;
    
    [SerializeField] private TextMeshProUGUI nameText;
    [SerializeField] private TextMeshProUGUI healthText;
    [SerializeField] private TextMeshProUGUI wrapRemainingText;
    [SerializeField] private TextMeshProUGUI wrapAssignedNumberText;
    [SerializeField] private TextMeshProUGUI assignAllText;
    
    [SerializeField] private Button upButton;
    [SerializeField] private Button downButton;
    [SerializeField] private Button startButton;
    private bool canAddWrap;
    private bool hasMinimumWrap;

    public void Set(string throwableName, int hp, int assigned, int remaining)
    {
        SetTexts(throwableName, hp, assigned, remaining);

        canAddWrap = remaining > 0;
        hasMinimumWrap = assigned > WrapManager.MinWrapAmount;
        SetButtons();
    }

    private void Update()
    {
        if (Inputs.IsKeyUp(Inputs.MoveLeft))
        {
            HandleLeftClicked();
        }

        if (Inputs.IsKeyUp(Inputs.MoveRight))
        {
            HandleRightClicked();
        }

        if (canAddWrap && Inputs.IsKeyUp(Inputs.Up))
        {
            HandleUpClicked();
        }

        if (hasMinimumWrap && Inputs.IsKeyUp(Inputs.Down))
        {
            HandleDownClicked();
        }
    }

    private void SetTexts(string throwableName, int hp, int assigned, int remaining)
    {
        nameText.text = throwableName;
        healthText.text = $"Health: {hp}";
        wrapAssignedNumberText.text = assigned.ToString();
        wrapRemainingText.text = $"Total wrap remaining: {remaining}";
    }

    private void SetButtons()
    {
        upButton.interactable = canAddWrap;
        downButton.interactable = hasMinimumWrap;
        startButton.interactable = !canAddWrap;
        assignAllText.gameObject.SetActive(canAddWrap);
    }

    public void HandleLeftClicked()
    {
        SelectionChanged?.Invoke(-1);
    }

    public void HandleRightClicked()
    {
        SelectionChanged?.Invoke(1);
    }

    public void HandleUpClicked()
    {
        AmountChanged?.Invoke(1);
    }

    public void HandleDownClicked()
    {
        AmountChanged?.Invoke(-1);
    }
    
    public void HandleStartClicked()
    {
        StartClicked?.Invoke();
    }
}