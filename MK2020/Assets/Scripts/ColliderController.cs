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

    public void Enable(Transform parentTransform, Vector2 position, ColliderData cd)
    {
        gameObject.SetActive(true);

        transform.SetParent(parentTransform);
        gameObject.transform.localPosition = position;

        type = cd.type;
        isWhite = cd.isWhite;
        collider.size = new Vector2(cd.size, 1.0f);
        collider.offset = new Vector2(cd.size * 0.5f - 0.5f, 0.0f);
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
