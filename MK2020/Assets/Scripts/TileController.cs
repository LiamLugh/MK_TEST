using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum TILETYPE { WHITE, BLACK, NONE };
public enum TILESUBTYPE { BOUNCE, NONE };

public class TileController : MonoBehaviour
{
    [SerializeField]
    Material[] mats;
    [SerializeField]
    Renderer myRenderer;
    [SerializeField]
    TILETYPE type = TILETYPE.NONE;
    [SerializeField]
    TILESUBTYPE subType = TILESUBTYPE.NONE;

    public void Enable(bool isWhite)
    {
        if(isWhite)
        {
            myRenderer.material = mats[0];
        }
        else
        {
            myRenderer.material = mats[1];
        }
        gameObject.SetActive(true);
    }

    public void Disable()
    {
        gameObject.SetActive(false);
    }
}
