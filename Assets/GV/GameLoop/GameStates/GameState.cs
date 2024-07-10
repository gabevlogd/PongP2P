namespace GV.GameLoop
{
    using UnityEngine;
    using GV.Patterns;

    public class GameState : StateBase<GameManager>
    {
        public GameState(string stateID, StateMachine<GameManager> stateMachine) : base(stateID, stateMachine)
        {
        }
    }
}


