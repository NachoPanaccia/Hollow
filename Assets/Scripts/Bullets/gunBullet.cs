using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class gunBullet : MonoBehaviour
{
    [SerializeField] private float bulletSpeed = 30f;
    [SerializeField] private float lifeTime = 3f;

    private float lifeTimer;
    private Vector3 m_direction = Vector3.right;

    public int damage = 5;
    public Pool bulletPool;

    private void OnEnable()
    {
        
        lifeTimer = lifeTime;
        
        m_direction = transform.right;
    }

    
    public void SetDirection(Vector3 direction)
    {
        if (direction.sqrMagnitude > 0.0001f)
            m_direction = direction.normalized;
        else
            m_direction = transform.right;
    }

    private void Update()
    {
        
        transform.position += m_direction * bulletSpeed * Time.deltaTime;

       
        lifeTimer -= Time.deltaTime;
        if (lifeTimer <= 0f)
        {
            if (bulletPool != null)
                bulletPool.ReturnToPool(gameObject);
            else
                Destroy(gameObject);
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
        }

       
        if (bulletPool != null)
            bulletPool.ReturnToPool(gameObject);
        else
            Destroy(gameObject);
    }
}
