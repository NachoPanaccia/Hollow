using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public class PlayerMover : MonoBehaviour
{
    [SerializeField] private MovementConfig config;
    [SerializeField] private MonoBehaviour inputSourceBehaviour;

    private IInputSource input;
    private Rigidbody2D rb;

    private bool dashing;
    private float dashTimer;
    private float dashCooldownTimer;
    private Vector2 dashDir;

    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        input = inputSourceBehaviour as IInputSource;
        if (config == null) Debug.LogWarning("PlayerMover: falta MovementConfig");
        if (input == null) Debug.LogError("PlayerMover: inputSourceBehaviour no implementa IInputSource");
    }

    private void Update()
    {
        if (dashCooldownTimer > 0f) dashCooldownTimer -= Time.deltaTime;

        if (dashing)
        {
            dashTimer -= Time.deltaTime;
            if (dashTimer <= 0f) dashing = false;
            return;
        }

        if (config != null && config.enableDash && dashCooldownTimer <= 0f && input.DashPressed)
        {
            Vector2 move = input.Move.sqrMagnitude > 0.01f ? input.Move.normalized : (Vector2)transform.up;
            dashing = true;
            dashDir = move;
            dashTimer = config.dashDuration;
            dashCooldownTimer = config.dashCooldown;
        }
    }

    private void FixedUpdate()
    {
        if (config == null || input == null) return;

        if (dashing)
        {
            rb.velocity = dashDir * config.dashSpeed;
            return;
        }

        Vector2 desired = input.Move.normalized * config.maxSpeed;
        Vector2 delta = desired - rb.velocity;

        float accel = (desired.sqrMagnitude > 0.01f) ? config.acceleration : config.deceleration;
        Vector2 change = Vector2.ClampMagnitude(delta, accel * Time.fixedDeltaTime);
        rb.velocity += change;
    }
}
