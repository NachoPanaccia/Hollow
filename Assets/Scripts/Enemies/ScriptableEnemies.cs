using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "EnemySO",menuName = "Scriptable/Enemy")]

public class ScriptableEnemies : ScriptableObject
{
    public int maxHealth;
    public float speed;
}
