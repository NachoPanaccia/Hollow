using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DañoHacha : MonoBehaviour
{
    public int damage = 1;
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

       
    }
}
