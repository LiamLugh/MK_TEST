using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DataSystem : MonoBehaviour
{
    [Header("Data")]
    [SerializeField]
    string seed = "";
    [SerializeField]
    bool useRandom = true;
    [SerializeField]
    uint highScore = 0;

    void OnAwake()
    {
        // Load highscoreData
    }

    public void SetSeed(string seed)
    {
        this.seed = seed;
        useRandom = false;
    }

    public void SetHighScore(uint highScore)
    {
        this.highScore = highScore;
    }

    public string GetSeed()
    {
        return seed;
    }

    public bool GetUseRandom()
    {
        return useRandom;
    }

    public uint GetHighScore()
    {
        return highScore;
    }

    public  void Reset()
    {
        seed = "";
        useRandom = true;
    }
}
