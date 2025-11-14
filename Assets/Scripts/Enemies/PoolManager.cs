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

        var pool = pools[prefab];

        PooledObject obj = null;

        // Buscar el primer objeto del pool que no esté destruido
        while (pool.Count > 0 && obj == null)
        {
            obj = pool.Dequeue();

            // Si Unity ya destruyó este objeto, va a comparar como null
            if (obj == null)
            {
                // Lo descartamos y seguimos buscando
                obj = null;
            }
        }

        // Si no encontramos ninguno “sano”, instanciamos uno nuevo
        if (obj == null)
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
    public static void ClearAll()
    {
        foreach (var kvp in pools)
        {
            var queue = kvp.Value;
            while (queue.Count > 0)
            {
                var obj = queue.Dequeue();
                if (obj != null)
                {
                    Object.Destroy(obj.gameObject);
                }
            }
        }
        pools.Clear();
    }
}
