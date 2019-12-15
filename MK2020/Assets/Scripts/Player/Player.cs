using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public delegate void GameOverEventHandler(EventArgs e);

public class Player : MonoBehaviour
{
    public event GameOverEventHandler gameOverEventHandler;

    [Header("Physics")]
    [SerializeField]
    Rigidbody2D rb = null;
    [Header("Rendering")]
    [SerializeField]
    Renderer myRenderer = null;
    [SerializeField]
    Renderer[] childRenderers = null;
    int childrenCount = 0;
    [SerializeField]
    Material[] mats = null;
    bool isWhite = true;
    float halfScreenWidth = 0.0f;
    [Header("Jump Stats")]
    [SerializeField]
    bool canJump = false;
    [SerializeField]
    float jumpPower = 0.0f;
    [SerializeField]
    float maxPower = 20.0f;
    [SerializeField]
    float minJumpPower = 15.0f;
    [SerializeField]
    float jumpPowerThreshold = 10.0f;
    [SerializeField]
    float powerMultiplier = 5.0f;
    Vector2 jumpVector = Vector2.zero;
    [Header("References")]
    [SerializeField]
    AudioSystem audioSystem = null;
    [SerializeField]
    ColourSensor colourSensor = null;
    [SerializeField]
    PlayerParticleColourContoller pController = null;

    // Pause System
    bool isPaused = false;
    Vector2 previousVelocity = Vector2.zero;
    float previousGravityScale = 0.0f;

    void Start()
    {
        childrenCount = childRenderers.Length;
        halfScreenWidth = Screen.width / 2;
        pController.SetColour(isWhite);
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.LeftControl) && !isWhite)
        {
            SetWhite();
        }
        else if (Input.GetKeyDown(KeyCode.RightControl) && isWhite)
        {
            SetBlack();
        }
        else if (Input.touchCount > 0)
        {
            Touch t = Input.GetTouch(0);
            if (t.position.x < halfScreenWidth && !isWhite)
            {
                SetWhite();
            }
            else if (t.position.x > halfScreenWidth && isWhite)
            {
                SetBlack();
            }
        }
    }

    void FixedUpdate()
    {
        if(!isPaused)
        {
            if(canJump)
            {
                if((!Input.anyKey && Input.touchCount == 0) && jumpPower > jumpPowerThreshold)
                {
                    // CLear current velocity
                    rb.velocity = Vector2.zero;

                    // If current jump power is less than minium jump power update it
                    if(jumpPower < minJumpPower)
                    {
                        jumpPower = minJumpPower;
                    }

                    // JUMP
                    jumpVector.Set(0.0f, jumpPower);
                    rb.AddForce(jumpVector, ForceMode2D.Impulse);

                    // Reset jump tracking
                    jumpPower = 0.0f;
                    canJump = false;
                    colourSensor.StopCountDown();

                    // Update Audio
                    audioSystem.PlayJumpSFX();
                    audioSystem.ResetWarningSFX();
                }
            }

            // Clearing jump power
            if (Input.GetKey(KeyCode.LeftControl) || Input.GetKey(KeyCode.RightControl) || Input.touchCount > 0)
            {
                jumpPower += powerMultiplier * Time.fixedDeltaTime;

                if (jumpPower > maxPower)
                {
                    jumpPower = maxPower;
                }

                audioSystem.UpdateWarningSFX(jumpPower / maxPower);
            }
            else
            {
                jumpPower = 0.0f;
                audioSystem.ResetWarningSFX();
            }
        }
    }

    public void SetJump(bool canJump)
    {
        this.canJump = canJump;
    }

    public bool GetCanJump()
    {
        return canJump;
    }

    void SetBlack()
    {
        myRenderer.material = mats[2];
        for (int i = 0; i < childrenCount; i++)
        {
            childRenderers[i].material = mats[3];
        }
        isWhite = false;
        pController.SetColour(isWhite);
        audioSystem.PlaySwitchSFX();
        colourSensor.CheckColour();
    }

    void SetWhite()
    {
        myRenderer.material = mats[0];
        for (int i = 0; i < childrenCount; i++)
        {
            childRenderers[i].material = mats[1];
        }
        isWhite = true;
        pController.SetColour(isWhite);
        audioSystem.PlaySwitchSFX();
        colourSensor.CheckColour();
    }

    public bool GetIsWhite()
    {
        return isWhite;
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

    // Game Over
    public void GameOver()
    {
        gameOverEventHandler?.Invoke(EventArgs.Empty);
        audioSystem.PlayGameOverSFX();
    }
}
