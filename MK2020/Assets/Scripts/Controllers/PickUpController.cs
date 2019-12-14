using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public delegate void PickUpPoolEventHandler(PickUpController p, bool colourCheck, EventArgs e);

public enum PickUpType { WHITE_COIN, BLACK_COIN, NONE };

public class PickUpController : MonoBehaviour
{
    public event PickUpPoolEventHandler poolThisPickUp;

    [SerializeField]
    Material[] mats = null;
    [SerializeField]
    Renderer myRenderer = null;
    [SerializeField]
    bool isWhite = true;
    [SerializeField]
    PickUpType type = PickUpType.NONE;
    [SerializeField]
    bool toScore = false;

    void OnTriggerEnter2D(Collider2D other)
    {
        if (!other.GetComponent<Player>()) return;
        bool colourCheck = (other.GetComponent<Player>().GetIsWhite() == isWhite);
        poolThisPickUp?.Invoke(this, colourCheck, EventArgs.Empty);
    }

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
        this.type = (isWhite) ? PickUpType.WHITE_COIN : PickUpType.BLACK_COIN;
    }

    public void Disable()
    {
        gameObject.SetActive(false);
    }

    public bool GetIsWhite()
    {
        return isWhite;
    }

    public PickUpType GetPickUpType()
    {
        return type;
    }

    public bool GetToScore()
    {
        return toScore;
    }
}
