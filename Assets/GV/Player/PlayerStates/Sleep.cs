namespace GV.Player
{
    using UnityEngine;
    using GV.Patterns;

    public class Sleep : PlayerState
    {
        public Sleep(string stateID, StateMachine<PlayerController> stateMachine) : base(stateID, stateMachine)
        {
        }
    }
}
