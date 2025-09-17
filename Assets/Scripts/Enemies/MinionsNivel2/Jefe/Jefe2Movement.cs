
using UnityEngine;

public class Jefe2Movement : MonoBehaviour
{
    [SerializeField] private ScriptableEnemies enemyData;
    [SerializeField] private LayerMask playerLayer;
    [SerializeField] private float detectTick = 0.15f;

    private float movementSpeed;
    public float detectionRadius = 10f;

    private Transform playerTransform;
    private bool playerDetected;
    private float nextDetectTime;
    private readonly Collider2D[] buffer = new Collider2D[4]; 

    void Start()
    {
        movementSpeed = (enemyData != null) ? enemyData.speed : 5f;  
        var player = GameObject.FindGameObjectWithTag("Player");
        if (player) playerTransform = player.transform;
    }

    void Update()
    {
        if (!playerDetected)
        {
            if (Time.time >= nextDetectTime) CheckPlayerDetection();
        }
        else
        {
            MoveTowardsPlayer();
        }
    }

    void CheckPlayerDetection()
    {
        int hits = Physics2D.OverlapCircleNonAlloc(transform.position, detectionRadius, buffer, playerLayer);
        for (int i = 0; i < hits; i++)
        {
            var c = buffer[i];
            if (c != null && c.CompareTag("Player")) { playerDetected = true; break; }
        }
        nextDetectTime = Time.time + detectTick;
    }

    void MoveTowardsPlayer()
    {
        if (!playerTransform) return;
        transform.position = Vector3.MoveTowards(transform.position, playerTransform.position, movementSpeed * Time.deltaTime);
    }
}
