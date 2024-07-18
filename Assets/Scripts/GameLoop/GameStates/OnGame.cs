using GV.Patterns;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OnGame : GameState
{
    public OnGame(string stateID, StateMachine<GameManager> stateMachine) : base(stateID, stateMachine)
    {
    }

    public override void OnEnter(GameManager context)
    {
        base.OnEnter(context);
        context.NetworkManager.StartGame();
    }
}
