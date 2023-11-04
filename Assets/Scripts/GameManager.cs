using MyStateMachine;
using UnityEngine;
using System;
using System.Collections;

public class GameManager : MonoBehaviour
{
    private StateMachine stateMachine;
    
    private State startState ,gameplayState ,winState ,loseState ,pauseState ,exitState;

    private float startAnimationTimer = 11.4f;
    [SerializeField] GameObject joystick;
    void Start()
    {
        var gameLoopStateMachineGraph = new Graph<State>();
        CreateState();
        CreateStateMachineGraph();
        stateMachine = new StateMachine(gameLoopStateMachineGraph, startState);
        stateMachine.StartMachine();


        void CreateState()
        {
            startState = new State(EnterStartState, () => { }, () => { });
            gameplayState = new State(EnterGamePlayState, UpdateGamePlayState, ExitGamePlayState);
            winState = new State(EnterWinState, () => { }, ExitWinState);
            loseState = new State(EnterLoseState, () => { }, ExitLoseState);
            pauseState = new State(EnterPauseState, () => { }, ExitPauseState);
            exitState = new State(EnterQuitState, () => { }, () => { });
        }
        
        void CreateStateMachineGraph()
        {
            gameLoopStateMachineGraph.AddVertex(startState);
            gameLoopStateMachineGraph.AddVertex(gameplayState);
            gameLoopStateMachineGraph.AddVertex(winState);
            gameLoopStateMachineGraph.AddVertex(loseState);
            gameLoopStateMachineGraph.AddVertex(pauseState);
            gameLoopStateMachineGraph.AddVertex(exitState);
        
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
    }

    IEnumerator StartDelay()
    {
        yield return new WaitForSeconds(startAnimationTimer);
        stateMachine.Transition(gameplayState);
    }

    private void Update()
    {
        stateMachine.Update();
    }

    void EnterStartState()
    {
        joystick.SetActive(false);
        StartCoroutine(StartDelay());
    }

    void EnterGamePlayState()
    {
        joystick.SetActive(true);
    }

    void UpdateGamePlayState()
    {
        
    }

    void ExitGamePlayState()
    {
         
    }

    void EnterWinState()
    {
         //show message
    }

    void ExitWinState()
    {
         //exit
    }

    void EnterLoseState()
    {
        //show message
    }

    void ExitLoseState()
    {
        //exit
    }

    void EnterPauseState()
    {
        Time.timeScale = 0;
        // TODO
        // if back to game button selected 
        // joystick.SetActive(true);
        // stateMachine.Transition(gamePlayState)
        // else if quit
        // Application.Quit();
    }

    void ExitPauseState()
    {
        Time.timeScale = 1;
    }
    void EnterQuitState()
    {
        Application.Quit();
    }
}