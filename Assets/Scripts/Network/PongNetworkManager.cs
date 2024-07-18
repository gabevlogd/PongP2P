using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Mirror;
using Steamworks;

public class PongNetworkManager : NetworkManager
{
    [HideInInspector] public List<PongPlayer> ConnectedPlayers;
    [SerializeField] private bool _useSteam = false;

    private Vector3 _leftSpawn = new Vector3(-22f, 0f, 0f);
    private Vector3 _rightSpawn = new Vector3(22f, 0f, 0f);

    public static event Action<List<LobbyData>> OnLobbyListFound;
    public static event Action OnLobbyListNotFound;
    public static event Action OnClientStarted;
    public static event Action OnServerDisconnected;
    public bool UseSteam { get => _useSteam; }


    public override void OnStartServer()
    {
        ConnectedPlayers = new List<PongPlayer>();
    }

    public override void OnServerAddPlayer(NetworkConnectionToClient conn)
    {
        base.OnServerAddPlayer(conn);
        PongPlayer player = conn.identity.GetComponent<PongPlayer>();
        ConnectedPlayers.Add(player);
    }

    public override void OnServerDisconnect(NetworkConnectionToClient conn)
    {
        PongPlayer player = conn.identity.GetComponent<PongPlayer>();
        ConnectedPlayers.Remove(player);
        base.OnServerDisconnect(conn);
        OnServerDisconnected?.Invoke();
    }

    public override void OnStartClient()
    {
        OnClientStarted?.Invoke();
    }


    public void HostLobby()
    {
        if (UseSteam)
        {
            SteamMatchmaking.CreateLobby(ELobbyType.k_ELobbyTypeFriendsOnly, maxConnections);
            return;
        }
        StartHost();
    }

    public void SearchLobbyList()
    {
        List<LobbyData> lobbies = new List<LobbyData>();
        for (int i = 0; i < SteamFriends.GetFriendCount(EFriendFlags.k_EFriendFlagImmediate); i++)
        {
            CSteamID friend = SteamFriends.GetFriendByIndex(i, EFriendFlags.k_EFriendFlagImmediate);
            
            if (SteamFriends.GetFriendGamePlayed(friend, out FriendGameInfo_t friendGameInfo))
            {
                if (friendGameInfo.m_gameID.ToString() == 480.ToString() && //480 = SpaceWar app id
                    friendGameInfo.m_steamIDLobby.IsValid() &&
                    friendGameInfo.m_steamIDLobby.IsLobby())
                {
                    LobbyData newLobbyData = new LobbyData(friendGameInfo.m_steamIDLobby, SteamFriends.GetFriendPersonaName(friend));
                    lobbies.Add(newLobbyData);
                }
            }
        }
        if (lobbies.Count > 0) OnLobbyListFound?.Invoke(lobbies);
        else OnLobbyListNotFound?.Invoke();
    }

    public void OpenGameplayScene()
    {
        if (ConnectedPlayers.Count < 2) return;
        ServerChangeScene("gameplay");
    }


    public void StartGame()
    {
        Vector3 spawnPosition = _leftSpawn;
        foreach (PongPlayer player in ConnectedPlayers)
        {
            GameObject playerRacket = GameObject.Instantiate(spawnPrefabs[0], spawnPosition, Quaternion.identity);
            NetworkServer.Spawn(playerRacket, player.connectionToClient);
            spawnPosition = _rightSpawn;
        }
        NetworkClient.localPlayer.GetComponent<PongPlayer>().RpcStartingGame();
    }

}
