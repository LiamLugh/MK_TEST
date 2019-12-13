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

        Debug.Log("SEND --- POOL THIS POOL");
        poolThisChunk?.Invoke(EventArgs.Empty);
    }
}
