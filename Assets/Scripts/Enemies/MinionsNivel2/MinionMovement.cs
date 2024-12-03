using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MinionMovement : MonoBehaviour
{
    [SerializeField] private ScriptableEnemies enemyData;
    //private float movementSpeed = 8f;
    private float movementSpeed;
    private float detectionRadius = 15f;
    public LayerMask playerLayer;

    private Transform playerTransform;
    private bool playerDetected = false;

    void Start()
    {
        movementSpeed = enemyData.speed;
        playerTransform = GameObject.FindGameObjectWithTag("Player").transform;
    }

    void Update()
    {
        if (!playerDetected)
        {
            CheckPlayerDetection();
        }
        else
        {
            MoveTowardsPlayer();
        }
    }

    void CheckPlayerDetection()
    {
        Collider2D[] colliders = Physics2D.OverlapCircleAll(transform.position, detectionRadius, playerLayer);

        foreach (Collider2D collider in colliders)
        {
            if (collider.CompareTag("Player"))
            {
                playerDetected = true;
            }
        }
    }

    void MoveTowardsPlayer()
    {
        transform.position = Vector3.MoveTowards(transform.position, playerTransform.position, movementSpeed * Time.deltaTime);
    }
}
