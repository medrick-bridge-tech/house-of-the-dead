using UnityEngine;

public class StateMachine : MonoBehaviour
{
    private BaseState CurrentState;
    public OnStartState StartState = new OnStartState();
    public OnGamePlayState GamePlay = new OnGamePlayState();
    public OnPauseState PauseState = new OnPauseState();
    public OnWinState WinState = new OnWinState();
    public OnLoseState LoseState = new OnLoseState();
    
    // Start is called before the first frame update
    void Start()
    {
        CurrentState = StartState;
        CurrentState.EnterState(this);
    }
    
    // Update is called once per frame
    void Update()
    {
        CurrentState.UpdateState(this);
    }

    void Exit()
    {
        CurrentState.ExitState(this);
    }
    
    public void SwitchState(BaseState state)
    {
        CurrentState = state;
        state.EnterState(this);
        
    }
}
