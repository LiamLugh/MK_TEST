using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TileSpawner : MonoBehaviour
{
    [SerializeField]
    GameObject PFB_floorTile = null;
    Queue<TileController> tilePool;

    void Awake()
    {
        tilePool = new Queue<TileController>();
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

    public void SpawnMap(byte[,] mapData, byte width, byte height)
    {
        Debug.Log("THIS HAPPENED MAPDATA " + mapData.Length + " WIDTH " + width + " HEIGHT " + height);
        for(int x = 0; x < width; x++)
        {
            for(int y = 0; y < height; y++)
            {
                byte dataAtLocation = mapData[x, y];

                if(dataAtLocation > 0)
                {
                    TileController tc = null;

                    if (tilePool.Count > 0)
                    {
                        Debug.Log("NOIPE");
                        tc = tilePool.Dequeue();
                    }
                    else
                    {
                        Debug.Log("YES");
                        GameObject newTile = Instantiate(PFB_floorTile, new Vector2(x, y), Quaternion.identity, transform);
                        tc = newTile.GetComponent<TileController>();
                    }

                    switch (dataAtLocation)
                    {
                        case 1:
                            tc.Enable(true);
                            break;
                        case 2:
                            tc.Enable(false);
                            break;
                        default:
                            break;
                    }
                }
            }
        }
    }
}
