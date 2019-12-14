using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public delegate void ChunkPoolEventHandler(EventArgs e);

public class ChunkTrigger : MonoBehaviour
{
    public event ChunkPoolEventHandler poolThisChunk;

    void OnTriggerEnter2D(Collider2D other)
    {
        if (!other.GetComponent<Player>()) return;
        poolThisChunk?.Invoke(EventArgs.Empty);
    }
}
