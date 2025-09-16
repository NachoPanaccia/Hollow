using UnityEngine;

public interface IInputSource
{
    Vector2 Move { get; }     // -1..1 X/Y
    bool DashPressed { get; }
}
