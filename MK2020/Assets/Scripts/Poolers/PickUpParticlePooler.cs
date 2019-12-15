using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PickUpParticlePooler : Pooler<ParticleColourController>
{
    [Header("References")]
    [SerializeField]
    LightSystem lightSystem = null;

    protected override void CreateAndPoolObject()
    {
        // Add new particle
        ParticleColourController p = Instantiate(prefab, transform);
        pool.Enqueue(p);

        // Set up particle and subscribe it's pool event
        SpawnedParticleController s = p.GetComponent<SpawnedParticleController>();
        s.SetParticleController(p);
        s.SetLightSystem(lightSystem);
        s.poolThisEventHandler += OnPoolThis;

        // Disable
        p.gameObject.SetActive(false);
    }

    private void OnPoolThis(ParticleColourController p, EventArgs e)
    {
        PoolObject(p);
    }
}
