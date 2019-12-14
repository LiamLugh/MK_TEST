using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PickUpPooler : Pooler<PickUpController>
{
    protected override void CreateAndPoolObject()
    {
        PickUpController p = Instantiate(prefab);
        pool.Enqueue(p);
        p.poolThisPickUp += OnPoolPickUpEvent;
    }

    void OnPoolPickUpEvent(PickUpController p, EventArgs e)
    {
        pool.Enqueue(p);
        p.Disable();
    }
}
