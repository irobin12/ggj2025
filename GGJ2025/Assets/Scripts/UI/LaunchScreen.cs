using UnityEngine;

public class LaunchScreen : MonoBehaviour
{
    [SerializeField] private ScoreUI scoreUI;

    public void SetTotalScore(int totalScore)
    {
        scoreUI.SetTotalScore(totalScore);
    }
}