namespace GV.Patterns
{
    using UnityEngine;

    /// <summary>
    /// State Machine to handle basic states
    /// </summary>
    /// <typeparam name="TContext">The type of the context</typeparam>
    public class StateMachine<TContext> 
    {
        private TContext _context;
        public StateBase<TContext> CurrentState { get => _currentState; }
        private StateBase<TContext> _currentState;
        public StateBase<TContext> PreviousState { get => _previousState; }
        private StateBase<TContext> _previousState;

        public StateMachine(TContext context)
        {
            _context = context;
        }

        /// <summary>
        /// Starts the state machine by passing the first state to execute
        /// </summary>
        /// <param name="entryPoint">First state to execute</param>
        public void Run(StateBase<TContext> entryPoint)
        {
            if (_currentState != null)
            {
                Debug.LogWarning($"{typeof(TContext)} State Machine already running");
                return;
            }

            _currentState = entryPoint;
            _currentState.OnEnter(_context);

        }

        /// <summary>
        /// Changes the current state to the passed state (only if the current state is not already the passed state)
        /// </summary>
        /// <param name="state">New state to execute</param>
        public void ChangeState(StateBase<TContext> state)
        {
            if (_currentState == state)
            {
                Debug.LogWarning($"{state.StateID} already executing");
                return;
            }

            _previousState = _currentState;
            _currentState.OnExit(_context);
            _currentState = state;
            _currentState.OnEnter(_context);
        }
    }
}


