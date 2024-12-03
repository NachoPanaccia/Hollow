using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class gunBullet : MonoBehaviour
{
    private float bulletSpeed = 30f;
    private Vector3 m_direction;
    public int damage = 5;
    public Pool bulletPool;

    void Start()
    {
        Destroy(gameObject, 3f);
    }

    public void SetDirection(Vector3 p_direction)
    {
        m_direction = p_direction;
    }

    void Update()
    {
        transform.position += bulletSpeed * Time.deltaTime * m_direction;
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        IDamageable target = collision.gameObject.GetComponent<IDamageable>();
        if (target != null)
        {
            ICommand damageCommand = new DamageCommand(target, damage);
            damageCommand.Execute();
        }
        
        bulletPool.ReturnToPool(this.gameObject);
    }
}