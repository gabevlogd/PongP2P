namespace GV.Patterns
{
    using UnityEngine;

    /// <summary>
    /// Basic state template 
    /// </summary>
    /// <typeparam name="TContext">The type of the context</typeparam>
    public abstract class StateBase<TContext>
    {
        public string StateID { get => _stateID; }
        private string _stateID;

        public StateMachine<TContext> StateMachine { get => _stateMachine; }
        private StateMachine<TContext> _stateMachine;

        public StateBase(string stateID, StateMachine<TContext> stateMachine)
        {
            _stateID = stateID;
            _stateMachine = stateMachine;
        }

        public virtual void OnEnter(TContext context)
        {
            Debug.Log("OnEnter " + _stateID);

        }

        public virtual void OnUpdate(TContext context)
        {
            //Debug.Log("OnUpadte " + _stateID);
        }

        public virtual void OnExit(TContext context)
        {
            //Debug.Log("OnExit " + _stateID);
        }
    }
}

