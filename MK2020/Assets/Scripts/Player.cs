using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    [SerializeField]
    Rigidbody2D rb = null;
    [SerializeField]
    Renderer myRenderer = null;
    [SerializeField]
    Renderer[] childRenderers = null;
    int childrenCount = 0;
    [SerializeField]
    Material[] mats = null;
    bool isWhiteMode = true;
    float halfScreenWidth = 0.0f;
    [SerializeField]
    float jumpPower = 0.0f;
    [SerializeField]
    float maxPower = 20.0f;
    [SerializeField]
    float minJumpPower = 15.0f;
    [SerializeField]
    float minPower = 10.0f;
    [SerializeField]
    float powerMultiplier = 5.0f;
    Vector2 jumpVector = Vector2.zero;
    [SerializeField]
    FloorSensor floorSensor = null;

    // Pause System
    bool isPaused = false;
    Vector2 previousVelocity = Vector2.zero;
    float previousGravityScale = 0.0f;


    void Start()
    {
        childrenCount = childRenderers.Length;
        halfScreenWidth = Screen.width / 2;
        jumpPower = minPower;
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.LeftControl) && !isWhiteMode)
        {
            SetWhite();
        }
        else if (Input.GetKeyDown(KeyCode.RightControl) && isWhiteMode)
        {
            SetBlack();
        }
        else if (Input.touchCount > 0)
        {
            Touch t = Input.GetTouch(0);
            if (t.position.x < halfScreenWidth && !isWhiteMode)
            {
                SetWhite();
            }
            else if (t.position.x > halfScreenWidth && isWhiteMode)
            {
                SetBlack();
            }
        }
    }

    void FixedUpdate()
    {
        if(!isPaused)
        {
            if(floorSensor.GetCanJump())
            {
                if((!Input.anyKey && Input.touchCount == 0) && jumpPower > minPower)
                {
                    rb.velocity = Vector2.zero;
                    if(jumpPower < minJumpPower)
                    {
                        jumpPower = minJumpPower;
                    }
                    jumpVector.Set(0.0f, jumpPower);
                    rb.AddForce(jumpVector, ForceMode2D.Impulse);
                    jumpPower = 0.0f;
                    floorSensor.SetCanJump(false);
                }
            }

            if (Input.GetKey(KeyCode.LeftControl) || Input.GetKey(KeyCode.RightControl) || Input.touchCount > 0)
            {
                jumpPower += powerMultiplier * Time.fixedDeltaTime;

                if (jumpPower > maxPower)
                {
                    jumpPower = maxPower;
                }
            }
            else
            {
                jumpPower = 0.0f;
            }
        }
    }

    void SetBlack()
    {
        myRenderer.material = mats[2];
        for (int i = 0; i < childrenCount; i++)
        {
            childRenderers[i].material = mats[3];
        }
        isWhiteMode = false;
    }

    void SetWhite()
    {
        myRenderer.material = mats[0];
        for (int i = 0; i < childrenCount; i++)
        {
            childRenderers[i].material = mats[1];
        }
        isWhiteMode = true;
    }

    // Pause System
    public void Pause()
    {
        isPaused = true;
        previousVelocity = rb.velocity;
        rb.velocity = Vector2.zero;
        previousGravityScale = rb.gravityScale;
        rb.gravityScale = 0.0f;
    }

    public void Unpause()
    {
        isPaused = false;
        rb.velocity = previousVelocity;
        rb.gravityScale = previousGravityScale;
    }
}
