using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using Mirror;
using Steamworks;
using System;

public class LobbyListMenu : MonoBehaviour
{
    [SerializeField] private GameObject _lobbiesScrollBox;
    [SerializeField] private FriendLobbySlot _friendLobbySlotPrefab;
    [SerializeField] private Button _refreshListButton;
    [SerializeField] private TextMeshProUGUI _noLobbyMessage;

    private PongNetworkManager _networkManager;

    private void Awake()
    {
        _networkManager = (PongNetworkManager)NetworkManager.singleton;
    }

    private void OnEnable()
    {
        PongNetworkManager.OnLobbyListFound += OnLobbyListFound;
        PongNetworkManager.OnLobbyListNotFound += OnLobbyListNotFound;
        _refreshListButton.onClick.AddListener(RefreshListClick);
    }
    private void OnDisable()
    {
        PongNetworkManager.OnLobbyListFound -= OnLobbyListFound;
        PongNetworkManager.OnLobbyListNotFound -= OnLobbyListNotFound;
        _refreshListButton.onClick.RemoveListener(RefreshListClick);
        DeleteLobbyList();
    }

    private void OnLobbyListFound(List<LobbyData> lobbyDataList)
    {
        DeleteLobbyList();
        _noLobbyMessage.gameObject.SetActive(false);
        InitializeLobbyList(lobbyDataList);
    }

    private void OnLobbyListNotFound()
    {
        _noLobbyMessage.gameObject.SetActive(true);
    }

    private void InitializeLobbyList(List<LobbyData> lobbyDataList)
    {
        foreach (LobbyData lobbyData in lobbyDataList)
        {
            FriendLobbySlot newSlot = Instantiate(_friendLobbySlotPrefab, _lobbiesScrollBox.transform);
            newSlot.InitializeLobbySlot(lobbyData);
        }
    }

    private void DeleteLobbyList()
    {
        //Debug.Log("DeleteLobbyList");
        List<Transform> children = new List<Transform>();
        for (int i = 0; i < _lobbiesScrollBox.transform.childCount; i++)
        {
            children.Add(_lobbiesScrollBox.transform.GetChild(i));
        }
        foreach (Transform child in children)
        {
            Destroy(child.gameObject);
        }
    }

    private void RefreshListClick() => _networkManager.SearchLobbyList();
}
