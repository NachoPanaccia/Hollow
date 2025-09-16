using System.Collections.Generic;
using UnityEngine;


public class BulletPool : MonoBehaviour, IObjectPool<GameObject>
{
    [SerializeField] private BulletConfig bulletConfig;
    [SerializeField][Min(1)] private int capacity = 10; // Tope duro


    private readonly Queue<GameObject> pool = new Queue<GameObject>();
    public int CountAvailable => pool.Count;
    public int Capacity => capacity;


    private void Awake()
    {
        if (bulletConfig == null || bulletConfig.bulletPrefab == null)
        {
            Debug.LogError("BulletPool: Falta BulletConfig o su bulletPrefab.");
            enabled = false;
            return;
        }


        // Pre-instancia exacto 'capacity' y no crea más
        for (int i = 0; i < capacity; i++)
        {
            var go = Instantiate(bulletConfig.bulletPrefab, transform);
            go.SetActive(false);
            // Asegurar que tenga BulletController
            var ctrl = go.GetComponent<BulletController>();
            if (ctrl == null)
            {
                Debug.LogError("El prefab de bala no tiene BulletController.");
                enabled = false;
                return;
            }
            ctrl.Initialize(bulletConfig, this);
            pool.Enqueue(go);
        }
    }


    public bool TryGet(out GameObject obj)
    {
        if (pool.Count > 0)
        {
            obj = pool.Dequeue();
            obj.SetActive(true);
            return true;
        }
        obj = null;
        return false; // No hay balas disponibles: respeta el tope de 10
    }


    public void Return(GameObject obj)
    {
        obj.SetActive(false);
        obj.transform.SetParent(transform);
        pool.Enqueue(obj);
    }
}