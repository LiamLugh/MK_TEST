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

    [Header("References")]
    [SerializeField]
    AudioSystem audioSystem = null;
    [SerializeField]
    DataSystem dataSystem = null;
    [SerializeField]
    PickUpParticlePooler particlePooler = null;

    void Awake()
    {
        dataSystem = GameObject.FindGameObjectWithTag("Data").GetComponent<DataSystem>();
        currentHighScore = dataSystem.GetHighScore();
    }

    internal void OnPoolPickUpEvent(PickUpController p, bool colourCheck, EventArgs e)
    {
        // Spawn particles
        ParticleColourController particle = particlePooler.GetObjectFromPool();
        particle.GetComponent<SpawnedParticleController>().StartTimer(p.transform.position, p.GetIsWhite());
        
        // Update score based on colour of pickup
        if(colourCheck)
        {
            currentScore += goodCoinValue;
            audioSystem.PlayGoodCoinSFX();
        }
        else
        {
            currentScore += badCoinValue;
            audioSystem.PlayBadCoinSFX();
        }

        UpdateScoreText(currentScore);

        if (currentScore > currentHighScore)
        {
            currentHighScore = currentScore;
            dataSystem.SetHighScore(currentHighScore);
            UpdateHighScoreText(currentHighScore);
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
        if (score < 1)
        {
            highScoreText.gameObject.SetActive(false);
            return;
        }

        highScoreText.gameObject.SetActive(true);

        highScoreText.text = "HI - " + score.ToString();
    }
}
