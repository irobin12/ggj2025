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
    [SerializeField] private Button startButton;

    public void Set(string throwableName, string hp, int assigned, int remaining)
    {
        nameText.text = throwableName;
        healthText.text = $"Health: {hp}";
        wrapAssignedNumberText.text = assigned.ToString();
        wrapRemainingText.text = remaining.ToString();
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