namespace MyStateMachine
{
    public class StateMachine
    {
        private State CurrentState;
        private Graph<State> states;

        public StateMachine(Graph<State> states, State initialState)
        {
            CurrentState = initialState;
            this.states = states;
        }

        public void StartMachine()
        {
            CurrentState.EnterState();
        }

        public void StopMachine()
        {
            CurrentState.ExitState();
        }

        public void Update()
        {
            if (CurrentState == null)
            {
                return;
            }

            CurrentState.UpdateState();
        }

        public void Transition(State nextState)
        {
            if (states.GetNeighbors(CurrentState).Contains(nextState) == false)
                return;
            
            CurrentState.ExitState();
            CurrentState = nextState;
            CurrentState.EnterState();
        }
    }
}