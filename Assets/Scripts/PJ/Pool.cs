using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pool : MonoBehaviour
{
    Queue<GameObject> objectPool;
    public GameObject ObjectPrefab;
    public int InitialObjects;

    void Awake()
    {
        objectPool = new Queue<GameObject>();

        for (int i = 0; i < InitialObjects; i++)
        {
            GameObject Object = Instantiate(ObjectPrefab);
            Object.SetActive(false);
            objectPool.Enqueue(Object);
        }
    }
    
    public GameObject GetObject()
    {
        GameObject Object;
        if (objectPool.Count > 0)
        {
            Object = objectPool.Dequeue();
            Object.SetActive(true);
        }
        else
        {
            Object = Instantiate(ObjectPrefab);
            objectPool.Enqueue(Object);
        }
        return Object;
    }

    public void ReturnToPool(GameObject Object)
    {
        Object.SetActive(false);
        objectPool.Enqueue(Object);
    }
}