using System;

namespace MyStateMachine
{
    public class State : Node
    {
        public event Action OnEnter;
        public event Action OnUpdate;
        public event Action OnExit;

        public State(Action onEnter, Action onUpdate, Action onExit)
        {
            OnEnter = onEnter;
            OnUpdate = onUpdate;
            OnExit = onExit;
        }

        public void EnterState() => OnEnter.Invoke();
        public void UpdateState() => OnUpdate.Invoke();
        public void ExitState() => OnExit.Invoke();
    }
}