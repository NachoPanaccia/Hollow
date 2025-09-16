using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyGunBullet : MonoBehaviour
{
    private float bulletSpeed = 30f;
    private Vector3 m_direction;
    public int damage = 1;

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
        if (collision.gameObject.CompareTag("Player"))
        {
            IDamageable player = collision.gameObject.GetComponent<IDamageable>();
            if (player != null)
            {
                // Ejecutar el comando de daño.
                ICommand damageCommand = new DamageCommand(player, damage);
                damageCommand.Execute();
            }
        }

        Destroy(gameObject); // Destruir la bala tras impactar.
    }

        
    
}