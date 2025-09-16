using UnityEngine;

public class PlayerAimer : MonoBehaviour
{
    [Header("Referencia (opcional)")]
    [SerializeField] private Transform pivot; // Si está vacío, usa this.transform

    [Header("Rotación")]
    [Tooltip("Si está activo, solo rota en Z (2D top-down)")]
    [SerializeField] private bool rotateZOnly = true;

    private Camera cam;

    private void Awake()
    {
        cam = Camera.main;
        if (pivot == null) pivot = transform;
        if (cam == null) Debug.LogWarning("PlayerAimer: no se encontró Camera.main");
    }

    private void Update()
    {
        if (cam == null) return;

        Vector3 mouseWorld = cam.ScreenToWorldPoint(Input.mousePosition);
        mouseWorld.z = pivot.position.z;

        Vector3 dir = (mouseWorld - pivot.position);
        if (dir.sqrMagnitude < 0.000001f) return;
        dir.Normalize();

        // Queremos que el 'up' del player apunte al mouse (coincide con balas que salen por transform.up)
        if (rotateZOnly)
        {
            float angle = Mathf.Atan2(dir.y, dir.x) * Mathf.Rad2Deg - 90f;
            pivot.rotation = Quaternion.Euler(0f, 0f, angle);
        }
        else
        {
            pivot.up = dir;
        }
    }
}
