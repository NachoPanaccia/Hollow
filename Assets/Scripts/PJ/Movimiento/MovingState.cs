using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovingState : IPlayerState
{
    public void EnterState()
    {
        Debug.Log("Entering Moving State");
    }

    public void UpdateState(Movimiento_PJ context)
    {
        Vector3 movementDirection = new Vector3(Input.GetAxisRaw("Horizontal"), Input.GetAxisRaw("Vertical"), 0f);

        if (movementDirection != Vector3.zero)
        {
            context.Move(movementDirection);
            RotatePlayer(context);
        }
        else
        {
            context.StopMoving();
            context.TransitionToState(new IdleState());
        }
    }

    public void ExitState()
    {
        Debug.Log("Exiting Moving State");
    }

    private void RotatePlayer(Movimiento_PJ context)
    {
        Vector3 mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        Vector3 directionToMouse = mousePosition - context.transform.position;
        float angle = Mathf.Atan2(directionToMouse.y, directionToMouse.x) * Mathf.Rad2Deg - 90f;
        context.transform.rotation = Quaternion.Euler(new Vector3(0, 0, angle));
    }
}