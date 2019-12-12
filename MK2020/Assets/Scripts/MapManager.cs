using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MapManager : MonoBehaviour
{
    [SerializeField]
    MapBuffer mapBuffer = null;
    [SerializeField]
    TileSpawner tileSpawner = null;
    [SerializeField]
    ColliderSpawner colliderSpawner = null;
    [SerializeField]
    RandomSystem random = null;

    void Start()
    {
        random.Init();
        mapBuffer.Init();
        tileSpawner.SpawnChunk(mapBuffer.GetMapData(), mapBuffer.GetWidth(), mapBuffer.GetHeight());
    }
}
