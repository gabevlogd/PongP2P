namespace GV.GameLoop
{
    using UnityEngine;
    using GV.Patterns;

    public class Play : GameState
    {
        public Play(string stateID, StateMachine<GameManager> stateMachine) : base(stateID, stateMachine) { }
    }
}
