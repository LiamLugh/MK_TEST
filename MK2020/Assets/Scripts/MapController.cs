using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MapController : MonoBehaviour
{
    [SerializeField]
    RandomSystem random = null;
    [SerializeField]
    TilePooler tilePool = null;
    [SerializeField]
    ColliderPooler colliderPool = null;
    [SerializeField]
    byte chunkWidth = 0;
    [SerializeField]
    byte chunkHeight = 0;

    // Chunk Plotting
    byte leadSpace = 0;
    Vector2Int currentTarget = Vector2Int.zero;
    byte startRampSize = 10;
    byte rampHeightDelta = 3;
    byte minHeight = 1;
    byte maxHeight = 6;
    byte heightOffset = 2;

    // Spawn config
    bool isWhite = true;
    byte colourCount = 0;

    // Map Data
    ChunkData mapChunk;
    [SerializeField]
    ChunkController[] chunkTransforms;
    byte chunkIndex = 0;
    byte activeChunkCount = 0;

    // Chunk movement
    bool canPoolChunks = false;
    float speed = 5.0f;
    Vector3 movementDelta = Vector3.zero;

    // Events
    [SerializeField]
    ChunkTrigger[] chunkTriggers;

    void Awake()
    {
        float heightf = Camera.main.orthographicSize * 2.0f;
        float widthf = heightf * Camera.main.aspect;

        leadSpace = (byte)(Mathf.CeilToInt(widthf) * 0.5f);

        chunkWidth = (byte)(leadSpace * 3);
        chunkHeight = (byte)Mathf.CeilToInt(heightf);

        activeChunkCount = (byte)chunkTransforms.Length;
    }

    void Start()
    {
        // Build Chunks

        // Subscribe To events
        for (int i = 0; i < activeChunkCount; i++)
        {
            chunkTriggers[i].poolThisChunk += OnChunkPoolEvent;
        }
    }

    void FixedUpdate()
    {
        MoveChunks();
    }

    void MoveChunks()
    {
        movementDelta = new Vector3(-speed * Time.fixedDeltaTime, 0.0f, 0.0f);

        for (int i = 0; i < activeChunkCount; i++)
        {
            chunkTransforms[i].transform.position += movementDelta;
        }
    }

    void PlotChunk()
    {
        List<TileController> tiles = new List<TileController>();
        List<ColliderController> colliders = new List<ColliderController>();


    }

    void UpdateChunkIndex()
    {
        chunkIndex = (byte)((chunkIndex + 1) % activeChunkCount);
    }

    // Event Subscriptions

    void OnChunkPoolEvent(ChunkData cd, EventArgs e)
    {
        if(canPoolChunks)
        {
            Debug.Log("REC --- POOL CHUNK! ");

            // Reposition oldest chunk
            chunkTransforms[chunkIndex].transform.position += Vector3.right * chunkWidth * 2;
            // Update current chunkIndex
            UpdateChunkIndex();
        }
        else
        {
            canPoolChunks = true;
        }
    }
}

public struct ChunkData
{
    public List<TileController> tiles;
    public List<ColliderController> colliders;

    public ChunkData(List<TileController> tiles, List<ColliderController> colliders)
    {
        this.tiles = tiles;
        this.colliders = colliders;
    }
}
