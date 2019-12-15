using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public delegate void PoolThisParticleEventHandler(ParticleColourController p, EventArgs e);

public class SpawnedParticleController : MonoBehaviour
{
    public event PoolThisParticleEventHandler poolThisEventHandler;

    [Header("References")]
    [SerializeField]
    LightSystem lightSystem = null;
    [SerializeField]
    ParticleColourController myParticleController = null;

    [Header("Stats")]
    float waitTime = 3.0f;

    public void StartTimer(Vector2 pos, bool isWhite)
    {
        StopAllCoroutines();
        // Eanble and start timer
        gameObject.SetActive(true);
        StartCoroutine(WaitTimer(pos, isWhite, waitTime));
    }

    IEnumerator WaitTimer(Vector2 pos, bool isWhite, float duration)
    {
        // Move to positon, set colour and light
        transform.position = pos;
        myParticleController.SetColour(isWhite);
        lightSystem.LightToPosition(pos, isWhite);

        float timer = 0.0f;

        while (timer <= duration)
        {
            timer += Time.deltaTime;
            yield return null;
        }

        // Send pool signal
        poolThisEventHandler?.Invoke(myParticleController, EventArgs.Empty);

        // Disable once pooled
        gameObject.SetActive(false);
    }

    public void SetParticleController(ParticleColourController p)
    {
        myParticleController = p;
    }

    public void SetLightSystem(LightSystem l)
    {
        lightSystem = l;
    }
}
