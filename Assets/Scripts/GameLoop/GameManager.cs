using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using GV.Patterns;
using Mirror;
using System;

/// <summary>
/// This is a server only class
/// </summary>
[RequireComponent(typeof(NetworkIdentity))]
public class GameManager : NetworkBehaviour
{
    public static GameManager Singleton { get => _singleton; }
    private static GameManager _singleton;
    public PongNetworkManager NetworkManager { get; private set; }

    #region State Machine;

    /// <summary>
    /// State machine for game states managing
    /// </summary>
    private StateMachine<GameManager> _stateMachine;
    public OnPreGame OnPreGame { get; private set; }
    public OnGame OnGame { get; private set; }
    public OnGameOver OnGameOver { get; private set; }

    #endregion

    private int _playerReadyCount;


    private void Awake()
    {
        if (_singleton == null) _singleton = this;
        else Destroy(gameObject);

        InitializeManager();
    }

    private void Update() => _stateMachine.CurrentState.OnUpdate(this);

    private void InitializeManager()
    {
        this.NetworkManager = (PongNetworkManager)Mirror.NetworkManager.singleton;

        _stateMachine = new StateMachine<GameManager>(this);
        OnPreGame = new OnPreGame("OnPreGame", _stateMachine);
        OnGame = new OnGame("OnGame", _stateMachine);
        OnGameOver = new OnGameOver("OnGameOver", _stateMachine);

        _stateMachine.Run(OnPreGame);
    }


    public void ChangeGameState(GameState newState)
    {
        _stateMachine.ChangeState(newState);
    }

    public GameState GetCurrentGameState() => (GameState)_stateMachine.CurrentState;


    public void SetPlayerReady(PongPlayer playerReady)
    {
        _playerReadyCount++;

        //Notify all the player that are not the player ready with a TargetRpc call
        foreach (PongPlayer player in NetworkManager.ConnectedPlayers)
        {
            if (player != playerReady) 
                player.RpcUpdateOpponentPlayerReady(player.connectionToClient);
        }
        
        //if all player are ready go to OnGame state
        if (_playerReadyCount == 2)
            ChangeGameState(OnGame);
    }

}
