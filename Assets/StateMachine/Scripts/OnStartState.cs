using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OnStartState :BaseState
{
    public override void EnterState(StateMachine state)
    {
        Debug.Log("Press S to start the game !");
    }

    public override void UpdateState(StateMachine state)
    {
        if (Input.GetKeyDown(KeyCode.S))
        {
            Debug.Log("The Game just Started!");
            state.SwitchState(state.GamePlay);
        }
    }

    public override void ExitState(StateMachine state)
    {
        Debug.Log("The Game just Start State is now over!");
    }
}