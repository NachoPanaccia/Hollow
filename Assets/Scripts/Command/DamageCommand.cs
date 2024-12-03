using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DamageCommand : ICommand
{
    private IDamageable target;
    private int damage;

    public DamageCommand(IDamageable target, int damage)
    {
        this.target = target;
        this.damage = damage;
    }

    public void Execute()
    {
        if (target != null)
        {
            Debug.Log($"Ejecutando DamageCommand. Daño: {damage}");
            target.TakeDamage(damage);
        }
        else
        {
            Debug.Log("Error: el objetivo de DamageCommand es nulo.");
        }
    }
}


