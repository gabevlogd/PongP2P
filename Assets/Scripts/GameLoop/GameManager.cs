using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using GV.Patterns;
using Mirror;
using System;

[RequireComponent(typeof(NetworkIdentity))]
public class GameManager : NetworkBehaviour
{
    private int _playerReady;
    public static GameManager Singleton { get => _singleton; }
    private static GameManager _singleton;

    public PongNetworkManager NetworkManager { get; private set; }

    private StateMachine<GameManager> _stateMachine;
    public OnPreGame OnPreGame { get; private set; }
    public OnGame OnGame { get; private set; }
    public OnGameOver OnGameOver { get; private set; }

    private void Awake()
    {
        if (_singleton == null) _singleton = this;
        else Destroy(gameObject);

        InitializeManager();
    }

    private void Update()
    {
        _stateMachine.CurrentState.OnUpdate(this);
        //Debug.Log(netId);
    }

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
        _playerReady++;
        foreach (PongPlayer player in NetworkManager.ConnectedPlayers)
        {
            if (player != playerReady) 
                player.RpcUpdateOpponentPlayerReady(player.connectionToClient);
        }
        
        if (_playerReady == 2)
            ChangeGameState(OnGame);
    }

    //[TargetRpc]
    //public void RpcUpdateOpponentPlayerReady(NetworkConnectionToClient target) => PongPlayer.OnUpdateOpponentPlayerReady?.Invoke();

}
