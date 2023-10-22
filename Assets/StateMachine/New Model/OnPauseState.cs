using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OnPauseState : BaseState
{
    public override void EnterState(StateManager state)
    {
        Debug.Log("Start Pause state");
    }

    public override void UpdateState(StateManager state)
    {
        Debug.Log("In Pause state");
    }

    public override void ExitState(StateManager state)
    {
        Debug.Log("Pause state is over");
    }
}

