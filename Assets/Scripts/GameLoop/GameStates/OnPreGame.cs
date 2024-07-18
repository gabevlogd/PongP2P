using GV.Patterns;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Mirror;

public class OnPreGame : GameState
{

    public OnPreGame(string stateID, StateMachine<GameManager> stateMachine) : base(stateID, stateMachine)
    {
    }
}
