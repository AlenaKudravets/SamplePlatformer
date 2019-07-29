using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class PlatformerStats
{
    public delegate void UpdateScoreDelegate(int newScore);

    public static event UpdateScoreDelegate CurrentScoreUpdated;//Bad bad code
    public static event UpdateScoreDelegate BestScoreUpdated;//use non static events pls

    private const string BestScoreKey = "BestScoreKey";

    private static int _currentScore = 0;


    /*

        public static int TestProperty3;
        public static int TestProperty { get; set; }

        private static int _testValue2;

        public static int TestProperty2
        {
            get
            {
                return _testValue2;
            }
            set { _testValue2 = value; }
        }*/

    public static int CurrentScore
    {
        get
        {
            return _currentScore;
        }
        set
        {
            _currentScore = value;

            if (_currentScore > BestScore)
            {
                BestScore = _currentScore;
            }

            if (CurrentScoreUpdated != null)
            {
                CurrentScoreUpdated(_currentScore);
            }
        }
    }

    public static int BestScore
    {
        get
        {
            return PlayerPrefs.GetInt(BestScoreKey, 0);
        }
        private set
        {
            PlayerPrefs.SetInt(BestScoreKey, value);
            PlayerPrefs.Save();

            if (BestScoreUpdated != null)
            {
                BestScoreUpdated(value);
            }
        }
    }


    public static void AddScore(int score)
    {
        CurrentScore = _currentScore + score;
    }
}
