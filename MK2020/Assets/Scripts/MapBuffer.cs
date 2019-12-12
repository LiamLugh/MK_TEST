using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MapBuffer : MonoBehaviour
{
    public GameObject pfb;
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

        for(int x = 0; x < width; x++)
        {
            Instantiate(pfb, new Vector2(x, height), Quaternion.identity, transform);
        }

        for (int y = 0; y < height; y++)
        {
            Instantiate(pfb, new Vector2(width, y), Quaternion.identity, transform);
        }
    }
}
