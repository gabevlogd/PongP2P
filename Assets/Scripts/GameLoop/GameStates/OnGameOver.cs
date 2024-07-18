using GV.Patterns;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OnGameOver : GameState
{
    public OnGameOver(string stateID, StateMachine<GameManager> stateMachine) : base(stateID, stateMachine)
    {
    }
}
