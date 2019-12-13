using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TileController : MonoBehaviour
{
    [SerializeField]
    Material[] mats = null;
    [SerializeField]
    Renderer myRenderer = null;

    public void Enable(Vector2 position, bool isWhite)
    {
        if(isWhite)
        {
            myRenderer.material = mats[0];
        }
        else
        {
            myRenderer.material = mats[1];
        }
        gameObject.transform.localPosition = position;
        gameObject.SetActive(true);
    }

    public void Disable()
    {
        gameObject.SetActive(false);
    }
}
