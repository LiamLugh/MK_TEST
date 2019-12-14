using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ColourSensor : MonoBehaviour
{
    [Header("References")]
    [SerializeField]
    Player player = null;
    [SerializeField]
    AudioSystem audioSystem = null;

    [Header("UI")]
    [SerializeField]
    Image countdownImage = null;

    [Header("GameOver Timer")]
    [SerializeField]
    float timeThreshold = 3.0f;
    [SerializeField]
    bool isCountingDown = false;

    ColliderController currentCollider = null;

    void OnTriggerEnter2D(Collider2D other)
    {
        ColliderController newCollider = other.GetComponent<ColliderController>();

        if (newCollider != null)
        {
            if (newCollider.GetColliderType() == ColliderType.FLOOR)
            {
                currentCollider = newCollider;
                CheckColour();
            }
        }
    }

    public void CheckColour()
    {
        if (currentCollider != null)
        {
            if (currentCollider.GetIsWhite() != player.GetIsWhite())
            {
                if (!isCountingDown)
                {
                    StartCoroutine(Countdown(timeThreshold));
                }
            }
            else
            {
                StopCountDown();
            }
        }
    }

    public void StopCountDown()
    {
        StopAllCoroutines();
        isCountingDown = false;
        audioSystem.ResetWarningSFX();
        countdownImage.fillAmount = 0.0f;
    }

    IEnumerator Countdown(float duration)
    {
        isCountingDown = true;
        float normalizedTime = 0;       // Normalised for the sound system

        while (normalizedTime <= 1f)
        {
            countdownImage.fillAmount = normalizedTime;
            normalizedTime += Time.deltaTime / duration;
            audioSystem.UpdateWarningSFX(normalizedTime);
            yield return null;
        }

        // End game if time over
        player.GameOver();
    }

}
