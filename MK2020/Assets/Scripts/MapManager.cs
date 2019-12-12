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

    void Start()
    {
        tileSpawner.SpawnTestZone(mapBuffer.GetWidth(), mapBuffer.GetHeight());
    }
}
