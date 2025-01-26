using TMPro;
using UnityEngine;

public class ScoreUI : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI totalScoreText;

    public void SetTotalScore(int totalScore)
    {
        totalScoreText.text = $"Total score: {totalScore}";
    }
}
