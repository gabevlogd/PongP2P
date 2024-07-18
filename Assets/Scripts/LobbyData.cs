using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Mirror;
using Steamworks;

public struct LobbyData
{
    public CSteamID LobbySteamID;
    public string LobbyName;

    public LobbyData(CSteamID lobbySteamID, string lobbyName)
    {
        LobbySteamID = lobbySteamID;
        LobbyName = lobbyName;
    }

    public bool IsValid() => LobbyName != null && LobbyName != "";
}
