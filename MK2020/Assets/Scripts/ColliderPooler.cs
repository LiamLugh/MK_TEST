using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ColliderPooler : Pooler<ColliderController>
{
    protected override void CreateAndPoolObject()
    {
        ColliderController c = Instantiate(prefab);
        pool.Enqueue(c);
    }
}
