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

    public void Enable(ColliderData cd)
    {
        gameObject.SetActive(true);

        type = cd.type;
        isWhite = cd.isWhite;
        gameObject.transform.position = cd.position;
        collider.size.Set(cd.size, 1.0f);
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
    public Vector2 position;
    public float size;
}
