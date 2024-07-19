using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using Mirror;
using Steamworks;
using System;

public class MainMenu : MonoBehaviour
{
    [SerializeField] private Button _hostButton;
    [SerializeField] private Button _joinButton;

    [SerializeField] private MenuManager _menuManager;
    private PongNetworkManager _networkManager;

    private void Awake()
    {
        _networkManager = (PongNetworkManager)NetworkManager.singleton;
    }

    private void OnEnable()
    {
        _hostButton.onClick.AddListener(HostButtonClick);
        _joinButton.onClick.AddListener(JoinButtonClick);
    }

    private void OnDisable()
    {
        _hostButton.onClick.RemoveListener(HostButtonClick);
        _joinButton.onClick.RemoveListener(JoinButtonClick);
    }

    private void HostButtonClick()
    {
        _networkManager.HostLobby();
        _menuManager.OpenMatchmakingMenu();
    }
    private void JoinButtonClick()
    {
        _menuManager.OpenLobbiesListMenu();
        _networkManager.SearchValidLobby();
    }

}
