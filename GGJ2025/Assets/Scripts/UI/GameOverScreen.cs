using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class GameOverScreen : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI scoreText;
    [SerializeField] private TextMeshProUGUI saveText;
    [SerializeField] private Button menuButton;

    public void SetTexts(int score, int save)
    {
        scoreText.text = $"Final score: {score}";
        saveText.text = $"You saved: {save}";
    }
    
    public void HandleMenuButton()
    {
        GameStatesManager.SetGameState(GameStatesManager.States.StartMenu);
    }
    
}