using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class gunBullet : MonoBehaviour
{
    [SerializeField] private float bulletSpeed = 30f;
    [SerializeField] private float lifeTime = 3f;

    private float lifeTimer;
    private Vector3 m_direction;

    public int damage = 5;
    public Pool bulletPool;

    void OnEnable()
    {
        lifeTimer = lifeTime;
    }

    public void SetDirection(Vector3 p_direction)
    {
        m_direction = p_direction.normalized;

        float angle = Mathf.Atan2(m_direction.y, m_direction.x) * Mathf.Rad2Deg - 90f;
        transform.rotation = Quaternion.Euler(0, 0, angle);
    }

    void Update()
    {
        transform.position += bulletSpeed * Time.deltaTime * m_direction;

        lifeTimer -= Time.deltaTime;
        if (lifeTimer <= 0f)
        {
            if (bulletPool != null) bulletPool.ReturnToPool(gameObject);
            else Destroy(gameObject);
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.TryGetComponent<IDamageable>(out var target))
        {
            ICommand damageCommand = new DamageCommand(target, damage);
            damageCommand.Execute();
        }

        if (collision.gameObject.CompareTag("Pared"))
        {
            Debug.Log("Choqué con la pared");

            if (bulletPool != null) bulletPool.ReturnToPool(gameObject);
            else Destroy(gameObject);

            return; // por las dudas, para no seguir haciendo nada
        }

        if (bulletPool != null) bulletPool.ReturnToPool(gameObject);
        else Destroy(gameObject);
    }
}
