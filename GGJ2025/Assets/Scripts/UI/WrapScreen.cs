using System;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class WrapScreen : MonoBehaviour
{
    public Action StartClicked;
    
    [SerializeField] private TextMeshProUGUI nameText;
    [SerializeField] private TextMeshProUGUI healthText;
    [SerializeField] private TextMeshProUGUI wrapRemainingText;
    [SerializeField] private TextMeshProUGUI wrapAssignedNumberText;
    [SerializeField] private TextMeshProUGUI assignAllText;
    [SerializeField] private Button startButton;
    
    public void HandleStartClicked()
    {
        StartClicked?.Invoke();
    }
}