using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ScoringSystem : MonoBehaviour
{
    [SerializeField]
    uint currentScore = 0;
    [SerializeField]
    uint currentHighScore = 0;

    // UI references
    [SerializeField]
    Text scoreText;
    [SerializeField]
    Text highScoreText;

    void Awake()
    {

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
