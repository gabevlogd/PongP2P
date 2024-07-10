namespace GV.Player
{
    using UnityEngine;
    using GV.Patterns;

    public class PlayerState : StateBase<PlayerController>
    {
        public PlayerState(string stateID, StateMachine<PlayerController> stateMachine) : base(stateID, stateMachine)
        {
        }
    }
}

