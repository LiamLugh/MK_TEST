using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TileController : MonoBehaviour
{
    [SerializeField]
    Material[] mats = null;
    [SerializeField]
    Renderer myRenderer = null;

    public void Enable(bool isWhite, Vector2 position)
    {
        if(isWhite)
        {
            myRenderer.material = mats[0];
        }
        else
        {
            myRenderer.material = mats[1];
        }
        gameObject.transform.position = position;
        gameObject.SetActive(true);
    }

    public void Disable()
    {
        gameObject.SetActive(false);
    }
}
