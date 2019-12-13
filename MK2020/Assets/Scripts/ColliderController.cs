using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ColliderController : MonoBehaviour
{
    [SerializeField]
    new BoxCollider2D collider = null;
    bool isWhite = false;
    [SerializeField]
    ColliderType type = ColliderType.NONE;

    public void Enable(Vector2 position, ColliderData cd)
    {
        gameObject.SetActive(true);

        gameObject.transform.position = position;

        type = cd.type;
        isWhite = cd.isWhite;
        collider.size.Set(cd.size, 1.0f);
        collider.offset.Set(cd.size * 0.5f, 0.0f);
    }

    public void Disable()
    {
        gameObject.SetActive(false);
    }

    public ColliderType GetColliderType()
    {
        return type;
    }
}

public enum ColliderType { FLOOR, PICKUP, NONE };

public struct ColliderData
{
    public ColliderType type;
    public bool isWhite;
    public float size;

    public ColliderData(ColliderType t, bool w, float s)
    {
        type = t;
        isWhite = w;
        size = s;
    }
}
