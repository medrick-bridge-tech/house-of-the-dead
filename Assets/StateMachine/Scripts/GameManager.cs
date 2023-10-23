using MyStateMachine;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    private State stateEvent;
    private StateMachine stateMachine;
    private bool isStarted = false;
    private State sleepState;
    private State startState;
    private State gameplayState;
    private State winState;
    private State loseState;
    private State pauseState;
    private State exitState;
    void Start()
    {
        sleepState = new State(EnterSleepState, UpdateSleepState, ExitSleepState);
        startState = new State(EnterStartState, UpdateStartState, ExitStartState);
        gameplayState = new State(EnterGamePlayState, UpdateGamePlayState, ExitGamePlayState);
        winState = new State(EnterWinState, UpdateWinState, ExitWinState);
        loseState = new State(EnterLoseState, UpdateLoseState, ExitLoseState);
        pauseState = new State(EnterPauseState, UpdatePauseState, ExitPauseState);
        exitState = new State(EnterQuitState, UpdateQuitState, ExitQuitState);
        
        var gameLoopStateMachineGraph = new Graph<State>();
        
        gameLoopStateMachineGraph.AddVertex(sleepState);
        gameLoopStateMachineGraph.AddVertex(startState);
        gameLoopStateMachineGraph.AddVertex(gameplayState);
        gameLoopStateMachineGraph.AddVertex(winState);
        gameLoopStateMachineGraph.AddVertex(loseState);
        gameLoopStateMachineGraph.AddVertex(pauseState);
        gameLoopStateMachineGraph.AddVertex(exitState);
        
        gameLoopStateMachineGraph.AddEdge(sleepState,startState);
        gameLoopStateMachineGraph.AddEdge(startState, gameplayState);
        gameLoopStateMachineGraph.AddEdge(startState,pauseState);
        gameLoopStateMachineGraph.AddEdge(gameplayState,winState);
        gameLoopStateMachineGraph.AddEdge(gameplayState,loseState);
        gameLoopStateMachineGraph.AddEdge(gameplayState,pauseState);
        gameLoopStateMachineGraph.AddEdge(pauseState,startState);
        gameLoopStateMachineGraph.AddEdge(pauseState,gameplayState);
        gameLoopStateMachineGraph.AddEdge(pauseState,exitState);
        gameLoopStateMachineGraph.AddEdge(winState,exitState);
        gameLoopStateMachineGraph.AddEdge(loseState,exitState);
        
        stateMachine = new StateMachine(gameLoopStateMachineGraph, sleepState);
    }

    private void Update()
    {
        stateMachine.Update();
        if (Input.GetKeyDown(KeyCode.S))
        {//is in sleep state
            if (!isStarted)
            {            
                stateMachine.Transition(startState);
                isStarted = true;
            }
            else
            {
                Debug.Log("Can Not change the state back to start");
                return;
            }
        }
        if(isStarted && Input.GetKeyDown(KeyCode.G))
            stateMachine.Transition(gameplayState);//start gameplay state
        if (!isStarted) 
        {// before start state
            if (Input.GetKeyDown(KeyCode.L) || Input.GetKeyDown(KeyCode.W))
                Debug.Log("Can Not change the state to win or lose");
            if(Input.GetKeyDown(KeyCode.P)) 
                stateMachine.Transition(pauseState);
        }
        else 
        {// is in Game Play state
            if(Input.GetKeyDown(KeyCode.W)) 
                stateMachine.Transition(winState);
            if(Input.GetKeyDown(KeyCode.L)) 
                stateMachine.Transition(loseState);
            if(Input.GetKeyDown(KeyCode.P)) 
                stateMachine.Transition(pauseState);
        }
        if(isStarted && Input.GetKeyDown(KeyCode.Q))
            stateMachine.Transition(exitState);//start gameplay state
    }

    void EnterSleepState()
    {
        Debug.Log("Sleep State Start");
    }

    void UpdateSleepState()
    {
        Debug.Log("Is In Sleep State");
    }

    void ExitSleepState()
    {
        Debug.Log("Sleep State exit");
    }
    
    void EnterStartState()
    {
        Debug.Log("Start State Start");
    }

    void UpdateStartState()
    {
        Debug.Log("Is In Start State");
    }

    void ExitStartState()
    {
        Debug.Log("Start State exit");
    }

    void EnterGamePlayState()
    {
        Debug.Log("GamePlay State Start");
    }

    void UpdateGamePlayState()
    {
        Debug.Log("Is In Gameplay State");
    }

    void ExitGamePlayState()
    {
        Debug.Log("GamePlay State exit");
    }

    void EnterWinState()
    {
        Debug.Log("Win State Start");
    }

    void UpdateWinState()
    {
        Debug.Log("Is In Win State");
    }

    void ExitWinState()
    {
        Debug.Log("Win State exit");
    }

    void EnterLoseState()
    {
        Debug.Log("Lose State Start");
    }

    void UpdateLoseState()
    {
        Debug.Log("Is In Lose State");
    }

    void ExitLoseState()
    {
        Debug.Log("Lose State exit");
    }

    void EnterPauseState()
    {
        Debug.Log("Pause State Start");
    }

    void UpdatePauseState()
    {
        Debug.Log("Is In Pause State");
    }

    void ExitPauseState()
    {
        Debug.Log("Pause State exit");
    }
    void EnterQuitState()
    {
        Debug.Log("Quit State start");
    }

    void UpdateQuitState()
    {
        Debug.Log("Is In Quit State");
    }

    void ExitQuitState()
    {
        Debug.Log("Quit State exit");
    }
}