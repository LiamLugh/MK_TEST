using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pooler<T> : MonoBehaviour
{
    // Unity apprently doesn't support serialisation of generic collections other than List<T>
    // [SerializeField]
    Queue<T> pool;
    [SerializeField]
    T pfb;

    void Start()
    {
        pool = new Queue<T>();
    }

    public T GetObjectFromPool()
    {
        if(GetPoolCount() > 0)
        {
            return pool.Dequeue();
        }

        T newObj = Instantiate(pfb);
        return newObj;
    }

    private T Instantiate(T pfb)
    {
        throw new NotImplementedException();
    }

    public void PoolObject(ref T obj)
    {
        pool.Enqueue(obj);
    }

    public void PoolObjectList(ref List<T> objList)
    {
        for(int i = 0; i < objList.Count; i++)
        {
            pool.Enqueue(objList[i]);
        }
    }

    public int GetPoolCount()
    {
        return pool.Count;
    }
}
