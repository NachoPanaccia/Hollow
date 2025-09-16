using UnityEngine;

[CreateAssetMenu(fileName = "MovementConfig", menuName = "Game/Player/Movement Config")]
public class MovementConfig : ScriptableObject
{
    [Header("Movimiento básico")]
    [Min(0f)] public float maxSpeed = 5f;
    [Min(0f)] public float acceleration = 30f;
    [Min(0f)] public float deceleration = 40f;

    [Header("Dash (opcional)")]
    public bool enableDash = false;
    [Min(0f)] public float dashSpeed = 12f;
    [Min(0f)] public float dashDuration = 0.15f;
    [Min(0f)] public float dashCooldown = 0.6f;
}
