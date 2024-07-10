namespace GV.Player
{
    using UnityEngine;
    using GV.Patterns;

    public class Idle : PlayerState
    {
        public Idle(string stateID, StateMachine<PlayerController> stateMachine) : base(stateID, stateMachine)
        {
        }
    }
}

