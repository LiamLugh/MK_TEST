using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChunkController : MonoBehaviour
{
    ChunkData data;

    public ChunkData GetData()
    {
        return data;
    }

    public void SetData(ChunkData data)
    {
        this.data = data;
    }
}
