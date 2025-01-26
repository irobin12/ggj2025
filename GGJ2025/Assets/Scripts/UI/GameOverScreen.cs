using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class GameOverScreen : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI scoreText;
    [SerializeField] private TextMeshProUGUI saveText;
    [SerializeField] private Button menuButton;

    public void SetTexts(int score, int save, int maxSaves)
    {
        scoreText.text = $"Final score: {score}";
        saveText.text = $"You saved: {save}/{maxSaves}";
    }
    
    public void HandleMenuButton()
    {
        GameStatesManager.SetGameState(GameStatesManager.States.StartMenu);
    }
    
}