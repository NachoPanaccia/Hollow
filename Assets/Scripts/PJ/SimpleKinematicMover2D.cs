using UnityEngine;

/// Motor cinemático 2D sin físicas: no usa Rigidbody2D ni AddForce.
/// - Acel/Desacel configurable
/// - Colisiones por BoxCast (probes), sin empujes ni fuerzas
/// - Slide opcional contra paredes
[DisallowMultipleComponent]
public class SimpleKinematicMover2D : MonoBehaviour
{
    [Header("Movimiento")]
    [SerializeField] private float maxSpeed = 12f;
    [SerializeField] private float acceleration = 40f;
    [SerializeField] private float deceleration = 50f;

    [Header("Colisión (probes)")]
    [SerializeField] private LayerMask obstacleMask;         // capas sólidas (paredes, props)
    [SerializeField] private Vector2 colliderSize = new Vector2(0.8f, 0.8f);
    [SerializeField] private Vector2 colliderOffset = Vector2.zero;
    [SerializeField] private float skinWidth = 0.04f;        // margen para no “pegarse”

    [Header("Slide")]
    [SerializeField] private bool slideAlongWalls = true;
    [SerializeField] private int maxSlideIterations = 1;

    [Header("Rotación (opcional)")]
    [SerializeField] private bool faceInputDirection = false;
    [SerializeField] private bool faceMouse = false;
    [SerializeField] private float rotationSpeed = 720f;

    // estado
    private Vector2 inputDir;      // intención (normalizada)
    private Vector2 velocity;      // vel propia (no física)

    public Vector2 CurrentVelocity => velocity;

    public void SetDirection(Vector2 dir)
    {
        inputDir = (dir.sqrMagnitude > 1f) ? dir.normalized : dir;
    }

    public void Stop()
    {
        inputDir = Vector2.zero;
    }

    void Update()
    {
        // 1) Rotación opcional (no afecta movimiento)
        if (faceMouse)
        {
            if (Camera.main != null)
            {
                Vector3 mouse = Camera.main.ScreenToWorldPoint(Input.mousePosition);
                LookAtPoint(mouse);
            }
        }
        else if (faceInputDirection && inputDir.sqrMagnitude > 0.0001f)
        {
            float angle = Mathf.Atan2(inputDir.y, inputDir.x) * Mathf.Rad2Deg - 90f;
            var targetRot = Quaternion.Euler(0, 0, angle);
            transform.rotation = Quaternion.RotateTowards(transform.rotation, targetRot, rotationSpeed * Time.deltaTime);
        }

        // 2) Acelerar/frenar hacia velocidad objetivo
        Vector2 targetVel = inputDir * maxSpeed;
        float rate = (targetVel.magnitude > velocity.magnitude && inputDir.sqrMagnitude > 0f) ? acceleration : deceleration;
        velocity = Vector2.MoveTowards(velocity, targetVel, rate * Time.deltaTime);

        // 3) Mover con barrido por ejes (X luego Y) usando BoxCast
        Vector2 delta = velocity * Time.deltaTime;
        MoveAxis(ref delta, true);   // X
        MoveAxis(ref delta, false);  // Y

        // 4) Slide opcional (sin ramas inalcanzables)
        if (slideAlongWalls && maxSlideIterations > 0 && inputDir.sqrMagnitude > 0.0001f)
        {
            Vector2 remaining = velocity * Time.deltaTime;
            if (remaining.sqrMagnitude > 0.000001f)
            {
                for (int i = 0; i < maxSlideIterations; i++)
                {
                    RaycastHit2D hit;
                    bool hitWall = BoxCast(transform.position, remaining.normalized, remaining.magnitude + skinWidth, out hit);
                    if (!hitWall) break;

                    // Proyección sobre la tangente del obstáculo
                    Vector2 n = hit.normal;
                    Vector2 t = Vector2.Perpendicular(n);
                    float alongT = Vector2.Dot(remaining, t);
                    if (Mathf.Abs(alongT) <= skinWidth) break;

                    Vector2 slideDelta = t * alongT;

                    RaycastHit2D hit2;
                    bool blocked = BoxCast(transform.position, slideDelta.normalized, Mathf.Abs(alongT) + skinWidth, out hit2);
                    if (!blocked)
                    {
                        transform.position += (Vector3)slideDelta;
                        remaining = Vector2.zero;
                        continue; // permitir otra iteración si configuraste > 1
                    }

                    // Si está bloqueado, no se puede deslizar más
                    break;
                }
            }
        }
    }

    // ---- helpers ----

    private void MoveAxis(ref Vector2 delta, bool moveX)
    {
        float dist = moveX ? Mathf.Abs(delta.x) : Mathf.Abs(delta.y);
        if (dist < 0.00001f)
        {
            return;
        }

        Vector2 dir = moveX ? new Vector2(Mathf.Sign(delta.x), 0f) : new Vector2(0f, Mathf.Sign(delta.y));

        RaycastHit2D hit;
        if (BoxCast(transform.position, dir, dist + skinWidth, out hit))
        {
            float allowed = Mathf.Max(0f, hit.distance - skinWidth);
            Vector3 step = (Vector3)(dir * allowed);
            transform.position += step;

            // anular componente bloqueada
            if (moveX) { delta.x = 0f; velocity.x = 0f; }
            else { delta.y = 0f; velocity.y = 0f; }
        }
        else
        {
            transform.position += (Vector3)(dir * dist);
            if (moveX) delta.x = 0f; else delta.y = 0f;
        }
    }

    private bool BoxCast(Vector3 fromPos, Vector2 dir, float dist, out RaycastHit2D hit)
    {
        Vector2 origin = (Vector2)fromPos + colliderOffset;
        hit = Physics2D.BoxCast(origin, colliderSize, 0f, dir, dist, obstacleMask);
        return hit.collider != null;
    }

    public void LookAtPoint(Vector3 worldPoint)
    {
        Vector2 to = (Vector2)(worldPoint - transform.position);
        if (to.sqrMagnitude < 0.0001f) return;
        float angle = Mathf.Atan2(to.y, to.x) * Mathf.Rad2Deg - 90f;
        var targetRot = Quaternion.Euler(0, 0, angle);
        transform.rotation = Quaternion.RotateTowards(transform.rotation, targetRot, rotationSpeed * Time.deltaTime);
    }

    // --- setters útiles desde el editor/otros scripts ---
    public void SetFaceMouse(bool v) => faceMouse = v;
    public void SetFaceInput(bool v) => faceInputDirection = v;
    public void SetObstacleMask(LayerMask m) => obstacleMask = m;
}
