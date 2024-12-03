using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyMovement : MonoBehaviour
{
    [SerializeField] private ScriptableEnemies enemyData;
    private float speed; 
    private float avoidanceRadius = 3f; 

    private Transform target;

    private void Start()
    {
        speed = enemyData.speed;
        GameObject player = GameObject.FindGameObjectWithTag("Player");
        if (player != null)
        {
            target = player.transform;
        }
    }

    private void Update()
    {
        RotacionYEvasion();
    }

    private void RotacionYEvasion()
    {
        if (target != null)
        {
            Vector3 direction = target.position - transform.position; 
            direction.Normalize(); 

            Collider2D[] obstacles = Physics2D.OverlapCircleAll(transform.position, avoidanceRadius);

            foreach (Collider2D obstacle in obstacles)
            {
                if (obstacle.CompareTag("Caja") | obstacle.CompareTag("Enemies Spawner"))
                {
                    Vector3 avoidDirection = transform.position - obstacle.transform.position;
                    direction += avoidDirection.normalized;
                }
            }

            float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg - 90f; 
            Quaternion targetRotation = Quaternion.Euler(0f, 0f, angle); 

            transform.rotation = Quaternion.RotateTowards(transform.rotation, targetRotation, 360f * Time.deltaTime); 

            transform.Translate(Vector3.up * speed * Time.deltaTime); 
        }
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, avoidanceRadius);
    }
}