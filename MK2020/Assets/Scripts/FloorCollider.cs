using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FloorCollider : MonoBehaviour
{
    [SerializeField]
    bool canJump = false;

    void OnTriggerEnter2D(Collider2D c)
    {
        canJump = true;
    }

    void OnTriggerExit2D(Collider2D c)
    {
        canJump = false;
    }

    public bool GetCanJump()
    {
        return canJump;
    }
}
