using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public delegate void ChunkPoolEventHandler(ChunkData data, EventArgs e);

public class ChunkTrigger : MonoBehaviour
{
    public event ChunkPoolEventHandler poolThisChunk;

    [SerializeField]
    ChunkData cd;

    void OnTriggerEnter2D(Collider2D other)
    {
        if (!other.GetComponent<Player>()) return;

        Debug.Log("THIS HAPPENED");
        poolThisChunk?.Invoke(cd, EventArgs.Empty);
    }
}
