using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerParticleColourContoller : MonoBehaviour
{
    [Header("References")]
    [SerializeField]
    ParticleColourController[] particleControllers = null;

    public void SetColour(bool isWhite)
    {
        for (int i = 0; i < particleControllers.Length; i++)
        {
            particleControllers[i].SetColour(isWhite);
        }
    }
}
