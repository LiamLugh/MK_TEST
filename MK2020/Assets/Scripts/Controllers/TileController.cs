using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TileController : MonoBehaviour
{
    [SerializeField]
    Material[] mats = null;
    [SerializeField]
    Renderer myRenderer = null;
    [SerializeField]
    bool isWhite = true;

    public void Enable(Transform parentTransform, Vector2 position, bool isWhite)
    {
        gameObject.SetActive(true);

        transform.SetParent(parentTransform);
        gameObject.transform.localPosition = position;
        
        if (isWhite)
        {
            myRenderer.material = mats[0];
        }
        else
        {
            myRenderer.material = mats[1];
        }

        this.isWhite = isWhite;
    }

    public void Disable()
    {
        gameObject.SetActive(false);
    }

    public bool GetIsWhite()
    {
        return isWhite;
    }
}
