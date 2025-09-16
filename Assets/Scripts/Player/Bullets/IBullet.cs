using UnityEngine;

public interface IBullet
{
    void Initialize(BulletConfig config, IObjectPool<GameObject> originPool);
    void SetDirection(Vector3 direction);
}