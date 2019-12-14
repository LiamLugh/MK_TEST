using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pooler<T> : MonoBehaviour
{
    protected Queue<T> pool;
    [SerializeField]
    protected T prefab;

    void Awake()
    {
        pool = new Queue<T>();
    }

    protected virtual void CreateAndPoolObject() {}

    public T GetObjectFromPool()
    {
        if(pool.Count == 0)
        {
            CreateAndPoolObject();
        }

        return pool.Dequeue();
    }

    public void PoolObject(T obj)
    {
        pool.Enqueue(obj);
    }

    public void PoolObjectList(T[] objList)
    {
        for(int i = 0; i < objList.Length; i++)
        {
            pool.Enqueue(objList[i]);
        }
    }
}
