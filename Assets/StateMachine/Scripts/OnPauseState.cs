using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OnPauseState : BaseState
{
    
    public override void EnterState(StateMachine state)
    {
        Debug.Log("Start Pause state");
    }

    public override void UpdateState(StateMachine state)
    {
        Debug.Log("In Pause state");
    }

    public override void ExitState(StateMachine state)
    {
        Debug.Log("Pause state is over");
    }
}

