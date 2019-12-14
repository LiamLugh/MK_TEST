using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerDeadZone : MonoBehaviour
{
    void OnTriggerEnter2D(Collider2D other)
    {
        if (!other.GetComponent<Player>()) return;
        other.GetComponent<Player>().GameOver();
    }
}
