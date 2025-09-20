using UnityEngine;
using System;


[DisallowMultipleComponent]
public class PooledObject : MonoBehaviour
{
    [SerializeField] private GameObject originPrefab;
    public GameObject OriginPrefab => originPrefab;

    public event Action<PooledObject> OnReleased;

    public void SetOrigin(GameObject prefab)
    {
        originPrefab = prefab;
    }

    public void Release()
    {
      
        OnReleased?.Invoke(this);
        PoolManager.Release(this);
    }
}
