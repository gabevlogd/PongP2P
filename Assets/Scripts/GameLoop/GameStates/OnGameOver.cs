using GV.Patterns;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Mirror;

public class OnGameOver : GameState
{
    public OnGameOver(string stateID, StateMachine<GameManager> stateMachine) : base(stateID, stateMachine)
    {
    }

    public override void OnEnter(GameManager context)
    {
        base.OnEnter(context);
        NetworkClient.localPlayer.GetComponent<PongPlayer>().RpcGameOver();
    }
}
