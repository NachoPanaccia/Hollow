using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IdleState : IPlayerState
{
    public void EnterState()
    {
        Debug.Log("Entering Idle State");
    }

    public void UpdateState(Movimiento_PJ context)
    {
        if (Input.GetKey(KeyCode.A) || Input.GetKey(KeyCode.D) || Input.GetKey(KeyCode.W) || Input.GetKey(KeyCode.S))
        {
            context.TransitionToState(new MovingState());
        }
        else
        {
            RotatePlayer(context);
        }

    }

    public void ExitState()
    {
        Debug.Log("Exiting Idle State");
    }

    private void RotatePlayer(Movimiento_PJ context)
    {
        Vector3 mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        Vector3 directionToMouse = mousePosition - context.transform.position;
        float angle = Mathf.Atan2(directionToMouse.y, directionToMouse.x) * Mathf.Rad2Deg - 90f;
        context.transform.rotation = Quaternion.Euler(new Vector3(0, 0, angle));
    }
}