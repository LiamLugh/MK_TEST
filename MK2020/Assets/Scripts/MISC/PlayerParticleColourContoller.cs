using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerParticleColourContoller : MonoBehaviour
{
    [Header("References")]
    [SerializeField]
    ParticleColourController[] particleControllers = null;
    [SerializeField]
    ParticleColourController explosionController = null;

    public void SetColour(bool isWhite)
    {
        for (int i = 0; i < particleControllers.Length; i++)
        {
            particleControllers[i].SetColour(isWhite);
        }
    }

    public void Explode(bool isWhite)
    {
        explosionController.SetColour(isWhite);
        explosionController.gameObject.SetActive(true);
        for (int i = 0; i < particleControllers.Length; i++)
        {
            particleControllers[i].gameObject.SetActive(false);
        }
    }
}
