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
    byte rampHeightDelta = 3;
    byte minHeight = 1;
    byte maxHeight = 3;
    byte heightOffset = 2;

    // Chunk spawn config
    bool isWhite = true;
    byte sinceColorChangeCount = 0;

    // Map Data
    ChunkData mapChunkData;
    [SerializeField]
    ChunkController[] chunkTransforms;
    byte chunkIndex = 0;
    byte activeChunkCount = 0;
    byte heightChangeChance = 25;
    byte gapChance = 10;
    byte minGapSize = 3;
    byte maxGapSize = 5;
    byte minPlatformLength = 1;

    // Chunk movement
    bool canPoolChunks = false;
    float speed = 5.0f;
    Vector3 movementDelta = Vector3.zero;
    byte chunkTriggerOffset = 13;

    // Events
    [SerializeField]
    ChunkTrigger[] chunkTriggers;

    void Awake()
    {
        // Initialise the random system
        random.Init();

        // Grab the width and height of devices screen in pixels
        float heightf = Camera.main.orthographicSize * 2.0f;
        float widthf = heightf * Camera.main.aspect;

        // Get the half screen length
        leadSpace = (byte)(Mathf.CeilToInt(widthf) * 0.5f);

        // Get the chunk pool trigger offset
        chunkTriggerOffset = (byte)chunkTriggers[0].transform.localPosition.x;

        // Set the width of each chunk to a screen and a half in length, so can reset smoothly
        chunkWidth = (byte)(leadSpace * 4);
        chunkHeight = (byte)Mathf.CeilToInt(heightf);

        activeChunkCount = (byte)chunkTransforms.Length;
    }

    void Start()
    {
        // Subscribe To events
        for (int i = 0; i < activeChunkCount; i++)
        {
            chunkTriggers[i].poolThisChunk += OnChunkPoolEvent;
            PlotChunk();    // Build Chunks - added in here to reduce to save a loop
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
        // Create new lists for chunk contents, to be assigned to ChunkData struct, for this particular chunk
        List<TileController> tiles = new List<TileController>();
        List<ColliderController> colliders = new List<ColliderController>();

        // Reset current plot target to left of the new chunk at whatever height it previously was
        currentTarget.Set(0, currentTarget.y);

        byte currentChunkTileCount = 0;
        byte currentPlatformLength = 0;

        // Loop over the horizontal positions of the chunk
        for (int i = 0; i < chunkWidth; i++)
        {
            byte x = (byte)currentTarget.x;
            byte y = (byte)currentTarget.y;

            /////////// ROLL FOR NEW HEIGHT, THEN GAP? THEN PLATFORM >>> LENGTH POSTION ETC ADD TO LISTS, UNTIL DONE, THEN ADD ALL TO CHUNKDATA

            // Roll for gap chance
            if (random.GetRandomInt(100) < gapChance)
            {
                // Generate gap length based on ranges defined at the top of file, that can fit within the bounds of the chunk
                byte gapLength = (byte)random.GetRandomInt(minGapSize, (maxGapSize < chunkWidth - y) ? maxGapSize : chunkWidth - y);
                // Skip loop forawrd by gap size
                i += gapLength;
            }
            else
            {
                // Rool for heightChange
                if (random.GetRandomInt(100) < heightChangeChance)
                {
                    y = (byte)random.GetRandomInt(y - 1, y + 2);

                    if (y < minHeight)
                    {
                        y = minHeight;
                    }
                    else if (y > maxHeight)
                    {
                        y = maxHeight;
                    }

                    // Update position for new height
                    currentTarget.Set(x, y);
                }

                // Generate new platform data
                byte maxLength = (byte)random.GetRandomInt(chunkWidth - currentTarget.x);
                if (maxLength <= minPlatformLength)
                {
                    maxLength = (byte)(minPlatformLength + 1);
                }
                currentPlatformLength = (byte)random.GetRandomInt(minPlatformLength, maxLength);

                // Update tracking vars
                currentChunkTileCount += currentPlatformLength;
                sinceColorChangeCount += currentPlatformLength;

                // Add Collider for platform, then add it to this chunks list of colliders
                ColliderController c = colliderPool.GetObjectFromPool();
                ColliderData cd = new ColliderData(ColliderType.FLOOR, isWhite, currentPlatformLength);
                c.Enable(chunkTransforms[chunkIndex].transform, currentTarget, cd);
                colliders.Add(c);

                // Add tiles
                for (int j = 0; j < currentPlatformLength; j++)
                {
                    TileController t = tilePool.GetObjectFromPool();
                    t.Enable(chunkTransforms[chunkIndex].transform, new Vector2(currentTarget.x + j, currentTarget.y), isWhite);
                    tiles.Add(t);
                }

                // Skip loop forward by platform length
                i += currentPlatformLength;
                // Update position with platform width
                x += currentPlatformLength;
                currentTarget.Set(x, y);

                // Roll to change colour
                if (random.GetRandomInt(100) < sinceColorChangeCount)
                {
                    isWhite = !isWhite;
                }
            }
        }

        // Create new chunk data and assign chunk data generated above
        mapChunkData = new ChunkData(tiles, colliders);
        chunkTransforms[chunkIndex].SetData(mapChunkData);
        UpdateChunkIndex();
    }

    void UpdateChunkIndex()
    {
        chunkIndex = (byte)((chunkIndex + 1) % activeChunkCount);
    }

    // Event Subscriptions

    void OnChunkPoolEvent(EventArgs e)
    {
        if(canPoolChunks)
        {
            // Reposition oldest chunk
            chunkTransforms[chunkIndex].transform.position += Vector3.right * (chunkWidth + chunkTriggerOffset);

            // Pool it's tiles and colliders
            TileController[] tControllers = chunkTransforms[chunkIndex].GetComponentsInChildren<TileController>();
            tilePool.PoolObjectList(tControllers);
            for (int i = 0; i < tControllers.Length; i++)
            {
                tControllers[i].transform.SetParent(tilePool.transform);
                tControllers[i].Disable();
            }
            ColliderController[] cControllers = chunkTransforms[chunkIndex].GetComponentsInChildren<ColliderController>();
            colliderPool.PoolObjectList(cControllers);
            for (int i = 0; i < cControllers.Length; i++)
            {
                cControllers[i].transform.SetParent(colliderPool.transform);
                cControllers[i].Disable();
            }

            // Plot new chunk
            PlotChunk();
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
