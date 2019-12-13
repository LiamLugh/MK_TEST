using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChunkController : MonoBehaviour
{
    ChunkData data;

    public ref List<TileController> GetTiles()
    {
        return ref data.tiles;
    }

    public ref List<ColliderController> GetColliders()
    {
        return ref data.colliders;
    }

    public void SetData(ChunkData data)
    {
        this.data = data;
    }
}
