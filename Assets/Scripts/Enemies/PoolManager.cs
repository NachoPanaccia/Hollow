using UnityEngine;
using System.Collections.Generic;

public static class PoolManager
{
    private static readonly Dictionary<GameObject, Queue<PooledObject>> pools =
        new Dictionary<GameObject, Queue<PooledObject>>();

    public static void Prewarm(GameObject prefab, int count)
    {
        if (!pools.ContainsKey(prefab))
            pools[prefab] = new Queue<PooledObject>();

        for (int i = 0; i < count; i++)
        {
            var inst = CreateNew(prefab);
            inst.gameObject.SetActive(false);
            pools[prefab].Enqueue(inst);
        }
    }

    public static GameObject Spawn(GameObject prefab, Vector3 position, Quaternion rotation, Transform parent = null)
    {
        if (!pools.ContainsKey(prefab))
            pools[prefab] = new Queue<PooledObject>();

        PooledObject obj;
        if (pools[prefab].Count > 0)
        {
            obj = pools[prefab].Dequeue();
        }
        else
        {
            obj = CreateNew(prefab);
        }

        var go = obj.gameObject;
        go.transform.SetParent(parent, false);
        go.transform.SetPositionAndRotation(position, rotation);
        go.SetActive(true);  
        return go;
    }

    public static void Release(PooledObject obj)
    {
        var prefab = obj.OriginPrefab;
        if (prefab == null)
        {
           
            Object.Destroy(obj.gameObject);
            return;
        }

        obj.gameObject.SetActive(false);
        obj.transform.SetParent(null, false);
        if (!pools.ContainsKey(prefab))
            pools[prefab] = new Queue<PooledObject>();
        pools[prefab].Enqueue(obj);
    }

    private static PooledObject CreateNew(GameObject prefab)
    {
        var go = Object.Instantiate(prefab);
        var po = go.GetComponent<PooledObject>();
        if (po == null) po = go.AddComponent<PooledObject>();
        po.SetOrigin(prefab);
        return po;
    }
}
