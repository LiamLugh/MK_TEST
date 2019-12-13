using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FloorSensor : MonoBehaviour
{
    [SerializeField]
    bool canJump = false;

    void OnTriggerEnter2D(Collider2D other)
    {
        ColliderController c = other.GetComponent<ColliderController>();

        if (c != null)
        {
            if (c.GetColliderType() == ColliderType.FLOOR)
            {
                canJump = true;
            }
        }
    }

    void OnTriggerExit2D(Collider2D other)
    {
        ColliderController c = other.GetComponent<ColliderController>();

        if(c != null)
        {
            if (c.GetColliderType() == ColliderType.FLOOR)
            {
                canJump = false;
            }
        }
    }

    public bool GetCanJump()
    {
        return canJump;
    }
}
