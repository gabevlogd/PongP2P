using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using Mirror;
using Steamworks;
using System;

public class MatchmakingMenu : MonoBehaviour
{
    [SerializeField] private Button _backButton;
    [SerializeField] private TextMeshProUGUI _matchmakingStatusMessage;

    [SerializeField] private MenuManager _menuManager;
    private PongNetworkManager _networkManager;

    private void Awake()
    {
        _networkManager = (PongNetworkManager)NetworkManager.singleton;
    }

    private void OnEnable()
    {
        _backButton.interactable = true;
        _backButton.onClick.AddListener(StopMatchmaking);
        PongPlayer.OnUpdatePlayerInfo += UpdateMatchmakingStatus;
        PongNetworkManager.OnServerDisconnected += UpdateMatchmakingStatus;
    }

    private void OnDisable()
    {
        _backButton.onClick.RemoveListener(StopMatchmaking);
        PongPlayer.OnUpdatePlayerInfo -= UpdateMatchmakingStatus;
        PongNetworkManager.OnServerDisconnected -= UpdateMatchmakingStatus;
    }

    private void StopMatchmaking()
    {
        if (NetworkServer.active && NetworkClient.isConnected)
            _networkManager.StopHost();
        else _networkManager.StopClient();
        _menuManager.OpenMainMenu();
    }

    private void UpdateMatchmakingStatus()
    {
        //Debug.Log("UpdateMatchmakingStatusMessage");
        _matchmakingStatusMessage.text = "";
        foreach (PongPlayer player in _networkManager.ConnectedPlayers)
        {
            _matchmakingStatusMessage.text += $"{player.GetName()}\n";
        }
        if (_networkManager.ConnectedPlayers.Count == 2)
        {
            _backButton.interactable = false;
            _matchmakingStatusMessage.text += "\nGame is starting...";
            if (NetworkServer.active) StartCoroutine(OpenGameplayScene(3f));
        }
    }

    private IEnumerator OpenGameplayScene(float Delay)
    {
        float startTime = Time.time;
        while(Time.time - startTime < Delay)
        {
            Debug.Log("starting game...");
            yield return null;
        }
        _networkManager.OpenGameplayScene();
    }
}
