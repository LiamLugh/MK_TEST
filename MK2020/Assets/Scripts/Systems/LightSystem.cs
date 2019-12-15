using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LightSystem : MonoBehaviour
{
    [Header("Light")]
    [SerializeField]
    Light pLight = null;
    [SerializeField]
    Color[] colours = null;

    [Header("Light Stats")]
    [SerializeField]
    float startIntensityTime = 0.2f;
    [SerializeField]
    float endIntensityTime = 5.0f;
    [SerializeField]
    float currentIntensity = 0.0f;
    [SerializeField]
    float maxIntensity = 10.0f;

    public void LightToPosition(Vector2 pos, bool isWhite)
    {
        // Stop any current light animations and reset
        StopAllCoroutines();
        currentIntensity = 0.0f;

        // Set new values
        pLight.transform.position = pos;
        pLight.color = (isWhite) ? colours[0] : colours[1];

        // Start new animation
        StartCoroutine(LightGlow(startIntensityTime, endIntensityTime));
    }

    IEnumerator LightGlow(float startDuration, float endDuration)
    {
        pLight.gameObject.SetActive(true);
        float timer = 0.0f;

        while (timer <= startDuration)
        {
            timer += Time.deltaTime;

            currentIntensity = Mathf.Lerp(0.0f, maxIntensity, timer / startDuration) * maxIntensity;
            pLight.intensity = currentIntensity;

            yield return null;
        }

        timer = 0.0f;

        while (timer <= endDuration)
        {
            timer += Time.deltaTime;

            currentIntensity = maxIntensity - (Mathf.Lerp(0.0f, maxIntensity, timer / endDuration) * maxIntensity);
            pLight.intensity = currentIntensity;

            yield return null;
        }

        pLight.gameObject.SetActive(false);
    }
}
