namespace GV.GameLoop
{
    using GV.Patterns;
    using UnityEngine;

    public class Pause : GameState
    {
        public Pause(string stateID, StateMachine<GameManager> stateMachine) : base(stateID, stateMachine)
        {
        }
    }
}

