using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Mover : MonoBehaviour
{
    [Header("Stats")]
    [SerializeField]
    float speed = 0.0f;
    [SerializeField]
    float maxSpeed = 0.0f;

    [Header("References")]
    [SerializeField]
    MapController mapController = null;

    void Start()
    {
        mapController = GameObject.FindGameObjectWithTag("MapController").GetComponent<MapController>();
        speed = mapController.GetChunkSpeed();
        maxSpeed = mapController.GetChunkMaxSpeed();
    }

    void FixedUpdate()
    {
        if(!mapController.GetIsPaused())
        {
            if (speed < maxSpeed)
            {
                speed = mapController.GetChunkSpeed();
            }

            transform.position += new Vector3(-speed * Time.fixedDeltaTime, 0.0f, 0.0f);
        }
    }
}
