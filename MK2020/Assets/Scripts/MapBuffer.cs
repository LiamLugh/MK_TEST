using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MapBuffer : MonoBehaviour
{
    [SerializeField]
    RandomSystem random = null;
    [SerializeField]
    byte[,] mapData = null;
    [SerializeField]
    byte width = 0;
    [SerializeField]
    byte height = 0;

    // Map configuration
    Vector2Int currentTarget = Vector2Int.zero;
    byte startRampSize = 5;
    byte minHeight = 1;

    // ColourData
    bool isWhite = true;
    ushort colourCount = 0;

    void Awake()
    {
        float heightf = Camera.main.orthographicSize * 2.0f;
        float widthf = heightf * Camera.main.aspect;

        width = (byte)(Mathf.CeilToInt(widthf) * 2);
        height = (byte)Mathf.CeilToInt(heightf);

        mapData = new byte[width, height];
    }

    public void InitMap()
    {
        // Start ramp
        for(int i = 0; i < startRampSize; i++)
        {
            mapData[i, minHeight] = 1;
            colourCount++;
        }

        currentTarget.Set(startRampSize, minHeight);

        for (int i = startRampSize; i < width; i++)
        {
            int newY = random.GetRandomInt(minHeight, currentTarget.y + 1);
            currentTarget.y = newY;

            // Work out wether it should be black or white
            int dice = random.GetRandomInt(100);

            if(dice < colourCount)
            {
                isWhite = !isWhite;
            }

            if(isWhite)
            {
                mapData[i, newY] = 1;
            }
            else
            {
                mapData[i, newY] = 2;
            }

            colourCount++;
        }
    }

    public byte GetWidth()
    {
        return width;
    }

    public byte GetHeight()
    {
        return height;
    }

    public byte[,] GetMapData()
    {
        return mapData;
    }
}
