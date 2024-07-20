using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Mirror;
using System;
using Steamworks;

public class PongPlayer : NetworkBehaviour
{
    [SyncVar]
    public string _playerName;
    [SyncVar]
    public CSteamID _playerSteamID;
    private PongNetworkManager _networkManager;

    public static event Action OnUpdatePlayerInfo;
    public static event Action OnUpdateOpponentPlayerReady;
    public static event Action OnStartingGame;
    public static event Action<GameFieldSide> OnPointScored;
    public static event Action OnGameOver;

    private void Awake()
    {
        _networkManager = (PongNetworkManager)NetworkManager.singleton;
    }

    public override void OnStartClient()
    {
        DontDestroyOnLoad(gameObject);
        if (authority)
        {
            _playerName = SteamFriends.GetPersonaName();
            _playerSteamID = SteamUser.GetSteamID();
            //TO DO: Rivedi questo punto...
            //CmdUpdatePlayerInfo(); //cosi a volte non funziona probabilmente per motivi di connessione 
            Invoke("CmdUpdatePlayerInfo", 2f); //dare tempo alle SyncVar di sincornizzarsi su tutti (con gli hook non mi stava funzionando lo stesso, non so cosa ho sbagliato, riprovare)
        }
        if (!isClientOnly) return;
        _networkManager.ConnectedPlayers.Add(this);

    }

    public override void OnStopClient()
    {
        if (!isClientOnly) return;
        _networkManager.ConnectedPlayers.Remove(this);
        OnUpdatePlayerInfo?.Invoke();
    }


    public string GetName() => _playerName;

    [Command]
    public void CmdUpdatePlayerInfo() => RpcUpdatePlayerInfo();

    [ClientRpc]
    public void RpcUpdatePlayerInfo() => OnUpdatePlayerInfo?.Invoke();

    [Command]
    public void CmdSetPlayerReady(PongPlayer playerReady) => GameManager.Singleton.SetPlayerReady(playerReady);

    [ClientRpc]
    public void RpcStartingGame() => OnStartingGame?.Invoke();

    [TargetRpc]
    public void RpcUpdateOpponentPlayerReady(NetworkConnectionToClient target) => OnUpdateOpponentPlayerReady?.Invoke();

    [ClientRpc]
    public void RpcScorePoint(GameFieldSide gameFieldSide) => OnPointScored?.Invoke(gameFieldSide);

    [ClientRpc]
    public void RpcGameOver() => OnGameOver?.Invoke();
    
    [Command]
    public void CmdSetPlayerReadyForNewGame() => GameManager.Singleton.SetPlayerReadyForNewGame();

}
