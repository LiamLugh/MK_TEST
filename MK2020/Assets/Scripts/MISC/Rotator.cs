using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum RotationAxis { XAXIS, YAXIS, ZAXIS };

public class Rotator : MonoBehaviour
{
    [SerializeField]
    float speed = 0.0f;
    [SerializeField]
    bool negate = false;
    [SerializeField]
    RotationAxis axis = RotationAxis.ZAXIS;

    void Start()
    {
        if(negate)
        {
            speed = -speed;
        }
    }

    void Update()
    {
        switch (axis)
        {
            case RotationAxis.XAXIS:
                transform.Rotate(speed * Time.deltaTime, 0.0f, 0.0f);
                break;
            case RotationAxis.YAXIS:
                transform.Rotate(0.0f, speed * Time.deltaTime, 0.0f);
                break;
            case RotationAxis.ZAXIS:
                transform.Rotate(0.0f, 0.0f, speed * Time.deltaTime);
                break;
            default:
                break;
        }
    }

    public void SetSpeed(float speed)
    {
        this.speed = speed;
    }
}
