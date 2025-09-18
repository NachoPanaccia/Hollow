using UnityEngine;

public class MovingState : IPlayerState
{
    public void EnterState() { }

    public void UpdateState(Movimiento_PJ context)
    {
        Vector2 input = new Vector2(Input.GetAxisRaw("Horizontal"), Input.GetAxisRaw("Vertical"));

        if (input.sqrMagnitude > 0.0001f)
        {
            context.SetMoveInput(input.normalized);
            context.RotateTowardsMouse();
        }
        else
        {
            context.StopMoving();
            context.TransitionToState(new IdleState());
        }
    }

    public void ExitState() { }
}
