using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TilePooler : Pooler<TileController>
{
    protected override void CreateAndPoolObject()
    {
        TileController t = Instantiate(prefab);
        pool.Enqueue(t);
    }
}
