using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LevelUIController : MonoBehaviour
{
    [SerializeField] private Text CurrentScore;
    [SerializeField] private Text BestScore;

    private void Start()
    {
        CurrentScore.text = "0";
        BestScore.text = PlatformerStats.BestScore.ToString();
    }

    private void OnEnable()
    {
        PlatformerStats.CurrentScoreUpdated += OnCurrentScoreUpdated;
        PlatformerStats.BestScoreUpdated += OnBestScoreUpdated;
    }

    private void OnDisable()
    {
        PlatformerStats.CurrentScoreUpdated -= OnCurrentScoreUpdated;
        PlatformerStats.BestScoreUpdated -= OnBestScoreUpdated;
    }

    private void OnCurrentScoreUpdated(int newscore)
    {
        CurrentScore.text = PlatformerStats.CurrentScore.ToString();
    }

    private void OnBestScoreUpdated(int newscore)
    {
        BestScore.text = PlatformerStats.BestScore.ToString();
    }


}
