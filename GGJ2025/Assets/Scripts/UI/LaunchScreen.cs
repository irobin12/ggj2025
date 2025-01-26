using UnityEngine;

public class LaunchScreen : MonoBehaviour
{
    [SerializeField] private ScoreUI scoreUI;

    public void SetTotalScore(int totalScore, int savedCount, int savedMax)
    {
        scoreUI.SetTotalScore(totalScore, savedCount, savedMax);
    }
}