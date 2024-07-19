using Mirror;
using Steamworks;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LobbyManager : MonoBehaviour
{
    private PongNetworkManager _networkManager;

    private const string _hostAddressKey = "HostAddress";

    private Callback<LobbyCreated_t> _lobbyCreated;
    private Callback<LobbyEnter_t> _gameLobbyEnter;
    private Callback<GameLobbyJoinRequested_t> _gameLobbyJoinRequested;

    public static event Action<LobbyCreated_t> OnLobbyCreated;
    public static event Action<LobbyEnter_t> OnLobbyEntered;
    public static event Action<GameLobbyJoinRequested_t> OnGameLobbyJoinRequested;

    void Start()
    {
        _networkManager = (PongNetworkManager)NetworkManager.singleton;

        if (!SteamManager.Initialized) return;

        _lobbyCreated = Callback<LobbyCreated_t>.Create(LobbyCreated);
        _gameLobbyEnter = Callback<LobbyEnter_t>.Create(LobbyEntered);
        _gameLobbyJoinRequested = Callback<GameLobbyJoinRequested_t>.Create(GameLobbyJoinRequested);
    }

    #region Steamworks callbacks: 

    private void LobbyCreated(LobbyCreated_t callback)
    {
        OnLobbyCreated?.Invoke(callback);

        if (callback.m_eResult != EResult.k_EResultOK) return;

        _networkManager.StartHost();
        SteamMatchmaking.SetLobbyData(new CSteamID(callback.m_ulSteamIDLobby), _hostAddressKey, SteamUser.GetSteamID().ToString());
        
    }

    private void LobbyEntered(LobbyEnter_t callback)
    {
        //Debug.Log("LobbyEntered");
        OnLobbyEntered?.Invoke(callback);

        if (NetworkServer.active) return;

        string hostAddress = SteamMatchmaking.GetLobbyData(new CSteamID(callback.m_ulSteamIDLobby), _hostAddressKey);
        _networkManager.networkAddress = hostAddress;
        _networkManager.StartClient();
    }

    private void GameLobbyJoinRequested(GameLobbyJoinRequested_t callback)
    {
        OnGameLobbyJoinRequested?.Invoke(callback);
        SteamMatchmaking.JoinLobby(callback.m_steamIDLobby);
    }

    #endregion


    
}
