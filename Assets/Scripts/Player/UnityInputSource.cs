using UnityEngine;

public class UnityInputSource : MonoBehaviour, IInputSource
{
    [Header("Bindings")]
    [SerializeField] private string horizontalAxis = "Horizontal";
    [SerializeField] private string verticalAxis = "Vertical";
    [SerializeField] private KeyCode dashKey = KeyCode.LeftShift;

    public Vector2 Move { get; private set; }
    public bool DashPressed { get; private set; }

    private void Update()
    {
        Move = new Vector2(Input.GetAxisRaw(horizontalAxis), Input.GetAxisRaw(verticalAxis));
        DashPressed = Input.GetKeyDown(dashKey);
    }
}
