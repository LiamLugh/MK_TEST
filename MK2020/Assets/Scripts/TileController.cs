using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum TILETYPE { WHITE, BLACK, NONE };
public enum TILESUBTYPE { BOUNCE, NONE };

public class TileController : MonoBehaviour
{
    [SerializeField]
    Material[] mats = null;
    [SerializeField]
    Renderer myRenderer = null;
    [SerializeField]
    TILETYPE type = TILETYPE.NONE;
    [SerializeField]
    TILESUBTYPE subType = TILESUBTYPE.NONE;

    public void Enable(bool isWhite)
    {
        if(isWhite)
        {
            myRenderer.material = mats[0];
            type = TILETYPE.WHITE;
        }
        else
        {
            myRenderer.material = mats[1];
            type = TILETYPE.BLACK;
        }
        gameObject.SetActive(true);
    }

    public void Disable()
    {
        gameObject.SetActive(false);
    }
}
