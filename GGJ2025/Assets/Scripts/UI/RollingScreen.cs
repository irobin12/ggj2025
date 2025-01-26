using UnityEngine;
using UnityEngine.UI;

public class RollingScreen : MonoBehaviour
{
    [SerializeField] private Image icon;
    [SerializeField] private ProgressBar healthBar;
    [SerializeField] private ProgressBar wrapBar;

    public void SetUp(Sprite sprite, int maxHealth, int maxWrap)
    {
        icon.sprite = sprite;
        
        healthBar.SetMinMaxValue(0, maxHealth);
        healthBar.SetFill(maxHealth);
        
        wrapBar.SetMinMaxValue(0, maxWrap);
        wrapBar.SetFill(maxWrap);
    }
}