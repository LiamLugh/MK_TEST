using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pooler<T> : MonoBehaviour
{
    protected Queue<T> pool;
    [SerializeField]
    protected T pfb;

    void Start()
    {
        pool = new Queue<T>();
    }

    protected virtual void CreateAndPoolObject()
    {

    }

    public T GetObjectFromPool()
    {
        if(GetPoolCount() == 0)
        {
            CreateAndPoolObject();
        }

        return pool.Dequeue();
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
