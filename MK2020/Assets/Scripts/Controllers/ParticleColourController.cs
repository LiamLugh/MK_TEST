using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEngine.ParticleSystem;

public class ParticleColourController : MonoBehaviour
{
    [Header("References")]
    [SerializeField]
    Color[] whiteColours = null;
    [SerializeField]
    Color[] blackColors = null;

    [Header("References")]
    [SerializeField]
    ParticleSystem p = null;

    public void SetColour(bool isWhite)
    {
        MainModule m = p.main;

        if(isWhite)
        {
            m.startColor = new MinMaxGradient(whiteColours[0], whiteColours[1]);
        }
        else
        {
            m.startColor = new MinMaxGradient(blackColors[0], blackColors[1]);
        }
    }
}
