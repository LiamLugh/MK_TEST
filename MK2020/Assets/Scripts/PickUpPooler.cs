using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PickUpPooler : Pooler<PickUpController>
{
    protected override void CreateAndPoolObject()
    {
        PickUpController p = Instantiate(pfb);
        pool.Enqueue(p);
    }
}
