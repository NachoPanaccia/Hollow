using UnityEngine;

public class EnemyMovement : MonoBehaviour
{
    [SerializeField] private ScriptableEnemies data;
    [SerializeField] private LayerMask obstacleMask; // setear paredes/cajas/spawners en capas
    [SerializeField] private float avoidanceTick = 0.15f; // [Materia: Expected Path] bajar frecuencia. 

    private float speed;
    private Transform target;
    private float nextAvoidTime;
    private readonly Collider2D[] overlapBuffer = new Collider2D[8]; // [Materia: Non-Alloc API]. 

    private Vector2 lastAvoidDir;

    private void Start()
    {
        speed = (data != null) ? data.speed : 3f;

        var player = GameObject.FindGameObjectWithTag("Player");
        if (player != null) target = player.transform;
    }

    private void Update()
    {
        if (target == null) return;

        Vector2 dir = (target.position - transform.position).normalized;

        if (Time.time >= nextAvoidTime)
        {
            // Sólo cada tick hacemos la evasión (no cada frame)
            int hits = Physics2D.OverlapCircleNonAlloc(transform.position,
                (data != null ? data.avoidanceRadius : 3f),
                overlapBuffer, obstacleMask); // [Materia: Non-Alloc + LayerMask]. 

            Vector2 avoid = Vector2.zero;
            for (int i = 0; i < hits; i++)
            {
                var c = overlapBuffer[i];
                if (c == null) continue;
                Vector2 away = (Vector2)(transform.position - c.bounds.ClosestPoint(transform.position));
                avoid += away.normalized;
            }
            lastAvoidDir = avoid;
            nextAvoidTime = Time.time + avoidanceTick;
        }

        if (lastAvoidDir != Vector2.zero)
        {
            float weight = (data != null ? data.avoidanceWeight : 1f);
            dir = (dir + lastAvoidDir.normalized * weight).normalized;
        }

        float angle = Mathf.Atan2(dir.y, dir.x) * Mathf.Rad2Deg - 90f;
        var targetRot = Quaternion.Euler(0, 0, angle);
        transform.rotation = Quaternion.RotateTowards(transform.rotation, targetRot, 360f * Time.deltaTime);

        transform.Translate(Vector3.up * speed * Time.deltaTime);
    }

    private void OnDrawGizmosSelected()
    {
        float r = data != null ? data.avoidanceRadius : 3f;
        Gizmos.color = new Color(1, 0.2f, 0.2f, 0.35f);
        Gizmos.DrawWireSphere(transform.position, r);
    }
}
