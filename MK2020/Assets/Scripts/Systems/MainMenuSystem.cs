using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainMenuSystem : MonoBehaviour
{
    [Header("References")]
    [SerializeField]
    ParticleColourController[] particleControllers = null;
    [SerializeField]
    ToggleSeedUI uiData = null;

    public void Play()
    {
        // Assign seed to singleton if needed
        if(uiData.GetUseSeed())
        {
            // grab the seed
        }
        else
        {
            // go with empty
        }
    }

    public void Quit()
    {
        Application.Quit();
    }
}
