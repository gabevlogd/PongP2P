using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using Mirror;
using Steamworks;

public class MenuManager : MonoBehaviour
{
    private enum MenuType { main, lobbyList, matchmaking }

    [SerializeField] private GameObject _mainMenu;
    [SerializeField] private GameObject _matchmakingMenu;
    [SerializeField] private GameObject _lobbyListMenu;

    private void Start() => OpenMainMenu();

    private void OpenMenu(MenuType menuTab)
    {
        _mainMenu.SetActive(menuTab == MenuType.main);
        _matchmakingMenu.SetActive(menuTab == MenuType.matchmaking);
        _lobbyListMenu.SetActive(menuTab == MenuType.lobbyList);
    }
    public void OpenMainMenu() => OpenMenu(MenuType.main);
    public void OpenMatchmakingMenu() => OpenMenu(MenuType.matchmaking);
    public void OpenLobbiesListMenu() => OpenMenu(MenuType.lobbyList);
    
    public void QuitGame() => Application.Quit();
}


