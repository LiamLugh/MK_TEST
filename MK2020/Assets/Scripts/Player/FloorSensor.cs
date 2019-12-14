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

    [Header("UI")]
    [SerializeField]
    Image countdownImage = null;

    [Header("GameOver Timer")]
    //[SerializeField]
    //float deathCountdown = 3.0f;
    [SerializeField]
    bool isCountingDown = false;

    void OnTriggerEnter2D(Collider2D other)
    {
        ColliderController c = other.GetComponent<ColliderController>();

        if (c != null)
        {
            if (c.GetColliderType() == ColliderType.FLOOR && !player.GetCanJump())
            {
                audioSystem.PlayLandingSFX();

                StopAllCoroutines();
                isCountingDown = false;
                player.SetJump(true);

                if(c.GetIsWhite() != player.GetIsWhite())
                {
                    if(!isCountingDown)
                    {
                        //StartCoroutine(Countdown(deathCountdown));
                    }
                }
            }
        }
    }

    IEnumerator Countdown(float duration)
    {
        isCountingDown = true;
        float normalizedTime = 0;       // Normalised for the sound system

        while (normalizedTime <= 1f)
        {
            countdownImage.fillAmount = normalizedTime;
            normalizedTime += Time.deltaTime / duration;
            yield return null;
        }

        // End game if time over
        player.GameOver();
    }
}
