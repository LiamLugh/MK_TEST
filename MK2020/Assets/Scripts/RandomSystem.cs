using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class RandomSystem : MonoBehaviour
{
    System.Random r;
    [SerializeField]
    int seed = 0;
    [SerializeField]
    uint tickCount = 0;
    List<uint> tickLog;

    public void Init(int seed = 0)
    {
        if(seed != 0)
        {
            r = new System.Random(seed);
            this.seed = seed;
        }
        else
        {
            r = new System.Random();
            this.seed = r.Next();
            r = new System.Random(seed);
        }

        tickLog = new List<uint>();
    }

    public int GetRandomInt()
    {
        tickCount++;
        return r.Next();
    }

    public int GetRandomIntTo(int max)
    {
        tickCount++;
        return r.Next(max);
    }

    public int GetRandomIntInRange(int min, int max)
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
