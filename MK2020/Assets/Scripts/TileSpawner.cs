using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TileSpawner : MonoBehaviour
{
    [SerializeField]
    GameObject PFB_floorTile;

    void Start()
    {
        
    }

    public void SpawnTestZone(byte width, byte height)
    {
        for (int x = 0; x < width; x++)
        {
            Instantiate(PFB_floorTile, new Vector2(x, height), Quaternion.identity, transform);
        }

        for (int y = 0; y < height; y++)
        {
            Instantiate(PFB_floorTile, new Vector2(width, y), Quaternion.identity, transform);
        }
    }
}
