using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pool : MonoBehaviour
{
    Queue<GameObject> objectPool;
    public GameObject ObjectPrefab;
    public int InitialObjects = 10;

    void Awake()
    {
        objectPool = new Queue<GameObject>();

        for (int i = 0; i < InitialObjects; i++)
        {
            GameObject obj = Instantiate(ObjectPrefab);
            obj.SetActive(false);
            objectPool.Enqueue(obj);
        }
    }

    public GameObject GetObject()
    {
        GameObject obj;
        if (objectPool.Count > 0)
        {
            obj = objectPool.Dequeue();
            obj.SetActive(true);
        }
        else
        {
            obj = Instantiate(ObjectPrefab);
            obj.SetActive(true);
        }
        return obj;
    }

    public void ReturnToPool(GameObject obj)
    {
        obj.SetActive(false);
        objectPool.Enqueue(obj);
    }
}
