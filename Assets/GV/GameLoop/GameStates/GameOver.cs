namespace GV.GameLoop
{
    using GV.Patterns;
    using UnityEngine;

    public class GameOver : GameState
    {
        public GameOver(string stateID, StateMachine<GameManager> stateMachine) : base(stateID, stateMachine)
        {
        }
    }
}

