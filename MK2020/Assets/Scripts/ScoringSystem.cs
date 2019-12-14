using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ScoringSystem : MonoBehaviour
{
    [Header("Scores")]
    [SerializeField]
    uint currentScore = 0;
    [SerializeField]
    uint currentHighScore = 0;
    [SerializeField]
    uint goodCoinValue = 10;
    [SerializeField]
    uint badCoinValue = 1;

    [Header("UI")]
    [SerializeField]
    Text scoreText = null;
    [SerializeField]
    Text highScoreText = null;

    void Awake()
    {
        // Get highscore data
    }

    internal void OnPoolPickUpEvent(PickUpController p, bool colourCheck, EventArgs e)
    {
        if(colourCheck)
        {
            currentScore += goodCoinValue;
        }
        else
        {
            currentScore += badCoinValue;
        }

        UpdateScoreText(currentScore);

        if (currentScore > currentHighScore)
        {
            UpdateHighScoreText(currentScore);
        }
    }

    void Start()
    {
        UpdateScoreText(currentScore);
        UpdateHighScoreText(currentHighScore);
    }

    void UpdateScoreText(uint score)
    {
        scoreText.text = score.ToString();
    }

    void UpdateHighScoreText(uint score)
    {
        if(score < 1)
        {
            highScoreText.gameObject.SetActive(false);
            return;
        }

        highScoreText.text = score.ToString();
    }
}
