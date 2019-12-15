using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MapController : MonoBehaviour
{
    [Header("Systems")]
    [SerializeField]
    RandomSystem random = null;
    [SerializeField]
    TilePooler tilePool = null;
    [SerializeField]
    ColliderPooler colliderPool = null;
    [SerializeField]
    PickUpPooler pickUpPool = null;

    // Chunk Plotting
    byte leadSpace = 0;
    Vector2Int currentTarget = Vector2Int.zero;
    byte minHeight = 1;
    byte maxHeight = 3;

    // Chunk spawn config
    bool isWhite = true;
    byte sinceColorChangeCount = 0;

    // Map Data
    ChunkData mapChunkData;
    [Header("Chunk Data")]
    [SerializeField]
    byte chunkWidth = 0;
    [SerializeField]
    byte chunkHeight = 0;
    byte heightOffset = 3;
    [SerializeField]
    ChunkController[] chunkTransforms = null;
    byte chunkIndex = 0;
    byte activeChunkCount = 0;
    byte heightChangeChance = 35;
    byte gapChance = 10;
    byte minGapSize = 2;
    byte maxGapSize = 5;
    byte minPlatformLength = 1;
    byte pickUpChance = 25;
    byte sincePickUpSpawnedCount = 0;
    byte pickUpSpawnBonus = 25;
    byte maxPickUpSpawnHeight = 7;

    // Chunk movement
    Vector2 chunkStartingPosition = Vector2.zero;
    bool canPoolChunks = false;
    [SerializeField]
    float speed = 2.0f;
    [SerializeField]
    float acceleration = 20.0f;
    [SerializeField]
    float maxSpeed = 20.0f;
    Vector3 movementDelta = Vector3.zero;
    byte chunkTriggerOffset = 13;
    byte chunkStartPosOffset = 8;

    // Pause System
    bool isPaused = false;

    [Header("Event Triggers")]
    [SerializeField]
    ChunkTrigger[] chunkTriggers = null;

    void Awake()
    {
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

        // Set pickupspawn bounds based on height
        maxPickUpSpawnHeight = (byte)(chunkHeight - heightOffset);

        activeChunkCount = (byte)chunkTransforms.Length;
    }

    void Start()
    {
        // Hard offset second chunk at start, and chunk starting position
        chunkTransforms[1].transform.position = new Vector2(chunkWidth, 0.0f);
        chunkStartingPosition = new Vector2(chunkWidth - chunkStartPosOffset, 0.0f);

        // Subscribe To events
        for (int i = 0; i < activeChunkCount; i++)
        {
            chunkTriggers[i].poolThisChunk += OnChunkPoolEvent;
            PlotChunk();    // Build Chunks - added in here to reduce to save a loop
        }
    }

    void FixedUpdate()
    {
        if(!isPaused)
        {
            UpdateSpeed();
            MoveChunks();
        }
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

            // Roll for gap chance
            if (random.GetRandomInt(100) < gapChance)
            {
                // Generate gap length based on ranges defined at the top of file, that can fit within the bounds of the chunk
                byte gapLength = (byte)random.GetRandomInt(minGapSize, (maxGapSize < chunkWidth - x) ? maxGapSize : chunkWidth - x);

                // Roll for pick ups across the length of the gap
                for (int j = 0; j < gapLength; j++)
                {
                    if (random.GetRandomInt(100) < pickUpChance + sincePickUpSpawnedCount + pickUpSpawnBonus)   // Increased chance over gaps
                    {
                        // Get a pickUp from the pool
                        PickUpController p = pickUpPool.GetObjectFromPool();

                        // Generate appropriate height to spawn pick up
                        byte minY = (y + 2 > maxPickUpSpawnHeight) ? maxPickUpSpawnHeight : (byte)(y + 2);
                        byte newY = (byte)random.GetRandomInt(minY, maxPickUpSpawnHeight);

                        Vector2 pos = new Vector2(x + j, newY);

                        // Enable it with data above
                        p.Enable(chunkTransforms[chunkIndex].transform, pos, isWhite);

                        // Update tracking
                        sincePickUpSpawnedCount = 0;
                    }
                    else
                    {
                        sincePickUpSpawnedCount++;
                    }
                }

                // Skip loop forawrd by gap size
                i += gapLength;
                // Update new X position target
                x += gapLength;
            }
            else
            {
                // Rool for heightChange
                if (random.GetRandomInt(100) < heightChangeChance)
                {
                    y = (byte)random.GetRandomInt(y - 1, y + 2);

                    // Adjust height for playfield bounds
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
                byte maxLength = (byte)(chunkWidth - x + 1);
                currentPlatformLength = (byte)random.GetRandomInt(minPlatformLength, maxLength);

                // Roll for pick ups across the length of the platform
                for (int j = 0; j < currentPlatformLength; j++)
                {
                    if (random.GetRandomInt(100) < pickUpChance + sincePickUpSpawnedCount)
                    {
                        // Get a pickUp from the pool
                        PickUpController p = pickUpPool.GetObjectFromPool();

                        // Generate appropriate height to spawn pick up
                        byte minY = (y + 1 > maxPickUpSpawnHeight) ? maxPickUpSpawnHeight : (byte)(y + 1);
                        byte newY = (byte)random.GetRandomInt(minY, maxPickUpSpawnHeight + 1);

                        Vector2 pos = new Vector2(x + j, newY);

                        // Enable it with data above
                        p.Enable(chunkTransforms[chunkIndex].transform, pos, isWhite);

                        // Update tracking
                        sincePickUpSpawnedCount = 0;
                    }
                    else
                    {
                        sincePickUpSpawnedCount++;
                    }
                }

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

                // Roll to change colour
                if (random.GetRandomInt(100) < sinceColorChangeCount)
                {
                    isWhite = !isWhite;
                }
            }

            // Set current target for adjusted X
            currentTarget.Set(x, y);
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

    void UpdateSpeed()
    {
        if(speed < maxSpeed)
        {
            speed += acceleration * Time.fixedDeltaTime;
        }
    }

    // Pause System
    public void Pause()
    {
        isPaused = true;
    }

    public void Unpause()
    {
        isPaused = false;
    }

    // Event Subscriptions
    void OnChunkPoolEvent(EventArgs e)
    {
        if(canPoolChunks)
        {
            // Reposition oldest chunk - the time it took to build the chunk to get rid of the occasional small gap
            chunkTransforms[chunkIndex].transform.position = chunkStartingPosition -= new Vector2(Time.fixedDeltaTime, 0.0f);

            // Pool it's tiles
            TileController[] tControllers = chunkTransforms[chunkIndex].GetComponentsInChildren<TileController>();
            tilePool.PoolObjectList(tControllers);
            for (int i = 0; i < tControllers.Length; i++)
            {
                tControllers[i].transform.SetParent(tilePool.transform);
                tControllers[i].Disable();
            }

            // And colliders
            ColliderController[] cControllers = chunkTransforms[chunkIndex].GetComponentsInChildren<ColliderController>();
            colliderPool.PoolObjectList(cControllers);
            for (int i = 0; i < cControllers.Length; i++)
            {
                cControllers[i].transform.SetParent(colliderPool.transform);
                cControllers[i].Disable();
            }

            // And any active PickUps
            PickUpController[] pControllers = chunkTransforms[chunkIndex].GetComponentsInChildren<PickUpController>();
            pickUpPool.PoolObjectList(pControllers);
            for (int i = 0; i < pControllers.Length; i++)
            {
                pControllers[i].transform.SetParent(pickUpPool.transform);
                pControllers[i].Disable();
            }

            // Plot new chunk
            PlotChunk();
        }
        else
        {
            canPoolChunks = true;
        }
    }

    // To give the light and pick up particles the appropriate movement illusion
    public float GetChunkSpeed()
    {
        return speed;
    }

    public float GetChunkMaxSpeed()
    {
        return maxSpeed;
    }

    public bool GetIsPaused()
    {
        return isPaused;
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
