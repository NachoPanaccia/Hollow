using UnityEngine;

public class IdleState : IPlayerState
{
    public void EnterState() { }

    public void UpdateState(Movimiento_PJ context)
    {
        // sin input: dejar que el motor frene
        bool any = Input.GetKey(KeyCode.A) || Input.GetKey(KeyCode.D) || Input.GetKey(KeyCode.W) || Input.GetKey(KeyCode.S);
        if (any)
        {
            context.TransitionToState(new MovingState());
            return;
        }

        context.SetMoveInput(Vector2.zero);
        context.RotateTowardsMouse();
    }

    public void ExitState() { }
}
