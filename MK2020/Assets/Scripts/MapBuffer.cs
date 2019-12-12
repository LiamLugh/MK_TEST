using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MapBuffer : MonoBehaviour
{
    [SerializeField]
    RandomSystem random = null;
    [SerializeField]
    byte[,] mapData = null;
    byte[,] currentSlice = null;
    [SerializeField]
    byte width = 0;
    [SerializeField]
    byte height = 0;

    // Map configuration
    byte bufferLead = 5;
    Vector2Int currentTarget = Vector2Int.zero;
    byte startRampSize = 10;
    byte minHeight = 1;
    byte heightOffset = 2;

    // Colliders
    ColliderData currentCollider;
    Queue<ColliderData> colliderQueue = null;

    // ColourData
    bool isWhite = true;
    byte colourCount = 0;

    void Awake()
    {
        float heightf = Camera.main.orthographicSize * 2.0f;
        float widthf = heightf * Camera.main.aspect;

        width = (byte)(Mathf.CeilToInt(widthf) + bufferLead);
        height = (byte)Mathf.CeilToInt(heightf);

        mapData = new byte[width, height];
        currentSlice = new byte[1, height];

        currentCollider = new ColliderData();
        colliderQueue = new Queue<ColliderData>();
    }

    public void Init()
    {
        // Start ramp
        for(int i = 0; i < startRampSize; i++)
        {
            mapData[i, minHeight] = 1;
            colourCount++;
        }

        // Add start ramp collider
        currentCollider.isWhite = isWhite;
        currentCollider.position = new Vector2((startRampSize * 0.5f) - 0.5f, minHeight);
        currentCollider.size = startRampSize;
        EnqueueCurrentCollider();

        currentTarget.Set(startRampSize, minHeight);

        for (int i = startRampSize; i < width; i++)
        {
            // Generate new height
            int newY = random.GetRandomInt(minHeight, currentTarget.y + heightOffset);

            // Update current collider
            if(newY != currentTarget.y)
            {
                currentCollider.position = currentTarget;
                EnqueueCurrentCollider();
            }
            else
            {
                currentCollider.size++;
            }

            // Update current target
            currentTarget.y = newY;

            // Work out wether it should be black or white
            int dice = random.GetRandomInt(100);

            if(dice < colourCount)
            {
                isWhite = !isWhite;
                EnqueueCurrentCollider();
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

    // Colliders

    void EnqueueCurrentCollider()
    {
        colliderQueue.Enqueue(currentCollider);
        currentCollider = new ColliderData();
        currentCollider.size = 1;
    }

    public Queue<ColliderData> GetColliderQueue()
    {
        return colliderQueue;
    }

    public int GetColliderCount()
    {
        return colliderQueue.Count;
    }

    // GETTERS

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
