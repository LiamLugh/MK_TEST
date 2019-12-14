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

    [Header("UI")]
    [SerializeField]
    Text scoreText = null;
    [SerializeField]
    Text highScoreText = null;

    void Awake()
    {
        // Get highscore data
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
