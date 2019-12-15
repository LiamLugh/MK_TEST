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
    bool isPaused = false;

    [Header("UI")]
    [SerializeField]
    Slider countdownSlider = null;
    [SerializeField]
    Image barImage = null;
    [SerializeField]
    Color startColour = Color.white;
    [SerializeField]
    Color endColour = Color.red;

    [Header("GameOver Timer")]
    [SerializeField]
    float timeThreshold = 3.0f;
    [SerializeField]
    bool isCountingDown = false;
    [SerializeField]
    int population = 0;

    ColliderController currentCollider = null;

    void OnTriggerEnter2D(Collider2D other)
    {
        ColliderController newCollider = other.GetComponent<ColliderController>();

        if (newCollider != null)
        {
            if (newCollider.GetColliderType() == ColliderType.FLOOR)
            {
                currentCollider = newCollider;
                population++;
                CheckColour();
            }
        }
    }

    void OnTriggerExit2D(Collider2D other)
    {
        ColliderController newCollider = other.GetComponent<ColliderController>();

        if (newCollider != null)
        {
            if (newCollider.GetColliderType() == ColliderType.FLOOR)
            {
                population--;
            }
        }
    }

    public void CheckColour()
    {
        if (currentCollider != null)
        {
            if (currentCollider.GetIsWhite() != player.GetIsWhite() && population > 0)
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
        countdownSlider.gameObject.SetActive(false);
        barImage.color = startColour;
        StopAllCoroutines();
        isCountingDown = false;
        countdownSlider.value = 0.0f;
    }

    IEnumerator Countdown(float duration)
    {
        countdownSlider.gameObject.SetActive(true);
        isCountingDown = true;
        float normalizedTime = 0;       // Normalised for the UI element

        while (normalizedTime <= 1f)
        {
            if(!isPaused)
            {
                countdownSlider.value = normalizedTime;
                normalizedTime += Time.deltaTime / duration;
                barImage.color = Color.Lerp(startColour, endColour, normalizedTime);
            }
            yield return null;
        }

        // End game if time over
        player.GameOver();
    }

    // Pause System
    public void Pause()
    {
        isPaused = true;
    }

    public void Unpause()
    {
        isPaused = false;
    }

}
