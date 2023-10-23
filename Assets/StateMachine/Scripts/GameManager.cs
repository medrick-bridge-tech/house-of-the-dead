using MyStateMachine;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    private StateMachine stateMachine;
    
    private bool isStandBy = true;
    
    private State standbyState ,startState ,gameplayState ,winState ,loseState ,pauseState ,exitState;

    void Start()
    {
        void CreateState()
        {
            standbyState = new State(EnterstandbyState, UpdatestandbyState, ExitstandbyState);
            startState = new State(EnterStartState, UpdateStartState, ExitStartState);
            gameplayState = new State(EnterGamePlayState, UpdateGamePlayState, ExitGamePlayState);
            winState = new State(EnterWinState, UpdateWinState, ExitWinState);
            loseState = new State(EnterLoseState, UpdateLoseState, ExitLoseState);
            pauseState = new State(EnterPauseState, UpdatePauseState, ExitPauseState);
            exitState = new State(EnterQuitState, UpdateQuitState, ExitQuitState);
        }

        var gameLoopStateMachineGraph = new Graph<State>();
        
        void CreateStateMachineGraph()
        {
            gameLoopStateMachineGraph.AddVertex(standbyState);
            gameLoopStateMachineGraph.AddVertex(startState);
            gameLoopStateMachineGraph.AddVertex(gameplayState);
            gameLoopStateMachineGraph.AddVertex(winState);
            gameLoopStateMachineGraph.AddVertex(loseState);
            gameLoopStateMachineGraph.AddVertex(pauseState);
            gameLoopStateMachineGraph.AddVertex(exitState);
        
            gameLoopStateMachineGraph.AddEdge(standbyState,startState);
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
        }
        CreateState();
        CreateStateMachineGraph();
        stateMachine = new StateMachine(gameLoopStateMachineGraph, standbyState);
    }

    private void Update()
    {
        stateMachine.Update();
        
        if (Input.GetKeyDown(KeyCode.S))
            stateMachine.Transition(startState);
        if (Input.GetKeyDown(KeyCode.G))
            stateMachine.Transition(gameplayState);
        if(Input.GetKeyDown(KeyCode.P))
            stateMachine.Transition(pauseState);
        if (Input.GetKeyDown(KeyCode.L))
            stateMachine.Transition(loseState);
        if (Input.GetKeyDown(KeyCode.W))
            stateMachine.Transition(winState);        
    }

    void EnterstandbyState()
    {
        Debug.Log("Sleep State Start");
    }

    void UpdatestandbyState()
    {
        Debug.Log("Is In Sleep State");
    }

    void ExitstandbyState()
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