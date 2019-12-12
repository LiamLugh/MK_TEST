using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ColliderSpawner : MonoBehaviour
{
    [SerializeField]
    GameObject PFB_floorCollider = null;
    Queue<ColliderController> colliderPool = null;

    void Awake()
    {
        colliderPool = new Queue<ColliderController>();
    }

    public void SpawnColliders(Queue<ColliderData> data)
    {
        while(data.Count > 0)
        {
            ColliderData cd = data.Dequeue();

            ColliderController cc = null;

            if (colliderPool.Count > 0)
            {
                cc = colliderPool.Dequeue();
            }
            else
            {
                GameObject newCollider = Instantiate(PFB_floorCollider, cd.position, Quaternion.identity, transform);
                cc = newCollider.GetComponent<ColliderController>();
            }

            Debug.Log("POSITION: (" + cd.position.x + "," + cd.position.y + ") - SIZE: " + cd.size + " - TYPE " + ((cd.isWhite) ? "WHITE" : "BLACK"));
            cc.Enable(cd);
        }
    }
}
