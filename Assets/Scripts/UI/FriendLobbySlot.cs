using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using Steamworks;

public class FriendLobbySlot : MonoBehaviour
{
    private LobbyData _data;
    [SerializeField] private TextMeshProUGUI _lobbyName;
    [SerializeField] private Button _joinButton;

    [SerializeField] private MenuManager _menuManager;

    public void InitializeLobbySlot(LobbyData data)
    {
        if (!data.IsValid())
        {
            Debug.LogWarning($"Invalid lobby data: {data}. FriendLobbySlot failed initialization");
            Destroy(gameObject);
            return;
        }

        _data = data;
        _lobbyName.text = data.LobbyName;
        _joinButton.onClick.RemoveAllListeners();
        _joinButton.onClick.AddListener(JoinButtonClick);
        _menuManager = gameObject.transform.parent.parent.parent.GetComponent<MenuManager>();
    }

    private void JoinButtonClick()
    {
        LobbyManager.CurrentLobbyID = _data.LobbySteamID;
        SteamMatchmaking.JoinLobby(_data.LobbySteamID);
        _menuManager.OpenMatchmakingMenu();
    }

}
