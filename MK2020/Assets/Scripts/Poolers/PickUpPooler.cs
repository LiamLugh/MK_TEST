using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PickUpPooler : Pooler<PickUpController>
{
    [SerializeField]
    ScoringSystem scoring = null;

    protected override void CreateAndPoolObject()
    {
        PickUpController p = Instantiate(prefab);
        pool.Enqueue(p);
        p.poolThisPickUp += OnPoolPickUpEvent;
        p.poolThisPickUp += scoring.OnPoolPickUpEvent;
    }

    void OnPoolPickUpEvent(PickUpController p, bool colourCheck, EventArgs e)
    {
        pool.Enqueue(p);
        p.Disable();
    }
}
