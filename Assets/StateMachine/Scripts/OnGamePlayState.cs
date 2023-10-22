using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OnGamePlayState : BaseState 
{
    
    public override void EnterState(StateMachine state)
    {
        // Play sound
        Debug.Log("Enter Game Play State");
    }
    
    public override void UpdateState(StateMachine state)
    {
        //Game Logic Runs here
        Debug.Log("Is in Game Play State");
        if (Input.GetKeyDown(KeyCode.W))
        {
            state.SwitchState(state.WinState);
        }

        if (Input.GetKeyDown(KeyCode.L))
        {
            state.SwitchState(state.LoseState);
        }
    }
    
    public override void ExitState(StateMachine state)
    {
        Debug.Log("Game Play state is now over");
    }
}