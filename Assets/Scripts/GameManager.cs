using MyStateMachine;
using UnityEngine;
using System;
using System.Collections;

public class GameManager : MonoBehaviour
{
    private StateMachine stateMachine;
    
    private State startState ,gameplayState ,winState ,loseState ,pauseState ,exitState;

    private float startAnimationTimer = 11.5f;

    private static GameManager instance;
    
    public static GameManager Instance
    {
        get { return instance; }
    }

    private void Awake()
    {
        instance = this;
    }

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
            winState = new State(EnterWinState, () => { }, () => { });
            loseState = new State(EnterLoseState, () => { }, () => { });
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
        UIManager.Instance.HideJoyStick();
        StartCoroutine(StartDelay());
    }

    void EnterGamePlayState()
    {
        UIManager.Instance.ShowJoyStick();
    }

    void UpdateGamePlayState()
    {
        
    }

    void ExitGamePlayState()
    {
         
    }

    void EnterWinState()
    {
        UIManager.Instance.ShowWinPanel();
    }

    void ExitWinState()
    {
         //exit
    }

    void EnterLoseState()
    {
        UIManager.Instance.ShowLosePanel();
    }

    void ExitLoseState()
    {
        //exit
    }

    void EnterPauseState()
    {
        Time.timeScale = 0;
        UIManager.Instance.HideJoyStick();
        UIManager.Instance.ShowPausePanel();
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
        UIManager.Instance.ShowJoyStick();
    }
    void EnterQuitState()
    {
        Application.Quit();
    }
}