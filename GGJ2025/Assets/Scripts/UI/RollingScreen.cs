using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class RollingScreen : MonoBehaviour
{
    [SerializeField] private Image icon;
    [SerializeField] private ProgressBar healthBar;
    [SerializeField] private ProgressBar wrapBar;
    [SerializeField] private ScoreUI scoreUI;

    public void SetUp(Sprite sprite, int maxHealth, int maxWrap)
    {
        icon.sprite = sprite;
        
        healthBar.SetMinMaxValue(0, maxHealth);
        healthBar.SetFill(maxHealth);
        
        wrapBar.SetMinMaxValue(0, maxWrap);
        wrapBar.SetFill(maxWrap);
    }

    public void ChangeValues(int health, int wrap)
    {
        healthBar.SetFill(health);
        wrapBar.SetFill(wrap);
    }

    public void SetTotalScore(int totalScore, int savedCount, int savedMax)
    {
        scoreUI.SetTotalScore(totalScore, savedCount, savedMax);
    }
}