using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IPlayerState
{
    void EnterState();
    void UpdateState(Movimiento_PJ context);
    void ExitState();
}