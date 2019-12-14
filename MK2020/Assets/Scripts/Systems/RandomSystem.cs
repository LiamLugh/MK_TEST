using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class RandomSystem : MonoBehaviour
{
    System.Random r;
    [SerializeField]
    string seed = "";
    [SerializeField]
    uint tickCount = 0;
    List<uint> tickLog;

    public void Init(string seed = "")
    {
        if(seed == "")
        {
            Debug.Log(Time.time.ToString());
            this.seed = Time.time.ToString();
        }
        else
        {
            this.seed = seed;
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
