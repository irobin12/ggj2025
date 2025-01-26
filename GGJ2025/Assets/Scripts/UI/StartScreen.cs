using System;
using TMPro;
using UnityEngine;

public class StartScreen : MonoBehaviour
{
    public Action WrapClicked;
    
    [SerializeField] private TextMeshProUGUI levelText;
    
    // This is awful but I'm just gonna do it 
    private string[] levelNames;
    private int level;

    private void Start()
    {
        SetLevelsText(LevelsManager.currentLevelName);
    }

    public void HandleLeftLevelClicked()
    {
        LevelsManager.SelectLevel(LevelsManager.currentLevelIndex - 1);
        SetLevelsText(LevelsManager.currentLevelName);
    }

    public void HandleRightLevelClicked()
    {
        LevelsManager.SelectLevel(LevelsManager.currentLevelIndex + 1);
        SetLevelsText(LevelsManager.currentLevelName);
    }

    public void HandleWrapClicked()
    {
        WrapClicked?.Invoke();
    }

    private void SetLevelsText(string levelName)
    {
        levelText.text = levelName;
    }
}