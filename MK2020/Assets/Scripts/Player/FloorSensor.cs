using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class FloorSensor : MonoBehaviour
{
    [Header("References")]
    [SerializeField]
    Player player = null;
    [SerializeField]
    AudioSystem audioSystem = null;

    void OnTriggerEnter2D(Collider2D other)
    {
        ColliderController c = other.GetComponent<ColliderController>();

        if (c != null)
        {
            if (c.GetColliderType() == ColliderType.FLOOR && !player.GetCanJump())
            {
                audioSystem.PlayLandingSFX();
                player.SetJump(true);
            }
        }
    }
}
