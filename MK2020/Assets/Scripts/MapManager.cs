using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MapManager : MonoBehaviour
{
    [SerializeField]
    MapBuffer mapBuffer;
    [SerializeField]
    TileSpawner tileSpawner;
    [SerializeField]
    ColliderSpawner colliderSpawner;
    [SerializeField]
    RandomSystem random;

    void Start()
    {
        random.Init();
        tileSpawner.SpawnTestZone(mapBuffer.GetWidth(), mapBuffer.GetHeight());
    }
}
