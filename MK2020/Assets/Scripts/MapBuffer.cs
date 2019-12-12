using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MapBuffer : MonoBehaviour
{
    [SerializeField]
    RandomSystem random;
    [SerializeField]
    byte[,] mapData;
    [SerializeField]
    byte width = 0;
    [SerializeField]
    byte height = 0;

    void Start()
    {
        float heightf = Camera.main.orthographicSize * 2.0f;
        float widthf = heightf * Camera.main.aspect;

        width = (byte)Mathf.CeilToInt(widthf);
        height = (byte)Mathf.CeilToInt(heightf);

        mapData = new byte[width, height];
    }

    public byte GetWidth()
    {
        return width;
    }

    public byte GetHeight()
    {
        return height;
    }
}
