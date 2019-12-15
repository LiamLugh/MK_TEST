using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class RandomSystem : MonoBehaviour
{
    [Header("Stats")]
    System.Random r;
    [SerializeField]
    bool useRandomSeed = false;
    [SerializeField]
    string seed = "";
    [SerializeField]
    uint tickCount = 0;
    List<uint> tickLog;

    [Header("References")]
    DataSystem dataSystem = null;

    void Awake()
    {
        dataSystem = GameObject.FindGameObjectWithTag("Data").GetComponent<DataSystem>();
        useRandomSeed = dataSystem.GetUseRandom();
        Init();
    }

    void Init()
    {
        if(useRandomSeed)
        {
            seed = DateTime.Now.ToString();
            dataSystem.SetSeed(seed);
        }
        else
        {
            seed = dataSystem.GetSeed();
        }

        r = new System.Random(seed.GetHashCode());

        tickLog = new List<uint>();
    }

    public int GetRandomInt()
    {
        tickCount++;
        return r.Next();
    }

    public int GetRandomInt(int max)
    {
        tickCount++;
        return r.Next(max);
    }

    public int GetRandomInt(int min, int max)
    {
        tickCount++;
        return r.Next(min, max);
    }

    public void LogTickCount()
    {
        tickLog.Add(tickCount);
        tickCount = 0;
    }
}
