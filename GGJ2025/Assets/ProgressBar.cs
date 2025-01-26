using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ProgressBar : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI text;
    [SerializeField] private Image fill;
    
    private int minValue;
    private int maxValue;

    public void SetMinMaxValue(int min, int max)
    {
        minValue = min;
        maxValue = max;
    }
    
    public void SetFill(int amount)
    {
        var lerpValue = Mathf.InverseLerp(minValue, maxValue, amount);
        fill.fillAmount = lerpValue;
        text.SetText($"{amount}/{maxValue}");
    }
}
