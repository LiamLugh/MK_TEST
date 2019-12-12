using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ColliderController : MonoBehaviour
{
    [SerializeField]
    new BoxCollider2D collider = null;
    bool isWhite = false;

    public void Enable(ColliderData cd)
    {
        gameObject.SetActive(true);

        isWhite = cd.isWhite;
        gameObject.transform.position = cd.position;
        collider.size.Set(cd.size, 1.0f);
    }

    public void Disable()
    {
        gameObject.SetActive(false);
    }
}

public struct ColliderData
{
    public bool isWhite;
    public Vector2 position;
    public float size;
}
