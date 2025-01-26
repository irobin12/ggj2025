using TMPro;
using UnityEngine;

public class ScoreUI : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI totalScoreText;
    [SerializeField] private TextMeshProUGUI savedText;

    public void SetTotalScore(int totalScore, int savedCount, int savedMax)
    {
        totalScoreText.text = $"Total score: {totalScore}";
        savedText.text = $"Saved: {savedCount}/{savedMax}";
    }
}
