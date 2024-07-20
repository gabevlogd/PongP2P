using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using Mirror;
using Steamworks;
using System;

public class HUD : MonoBehaviour
{
    // Left is HOST
    // Right is CLIENT
    [SerializeField] private TextMeshProUGUI _leftScore;
    [SerializeField] private TextMeshProUGUI _rightScore;
    [SerializeField] private TextMeshProUGUI _leftStatus;
    [SerializeField] private TextMeshProUGUI _rightStatus;
    [SerializeField] private TextMeshProUGUI _countdown;
    [SerializeField] private TextMeshProUGUI _gameOver;
    [SerializeField] private Button _readyButton;
    [SerializeField] private Button _newGameButton;

    private PongNetworkManager _networkManager;
    private GameManager _gameManager;

    private PongPlayer _localPlayer;

    private void Awake()
    {
        _networkManager = (PongNetworkManager)NetworkManager.singleton;
        _gameManager = GameManager.Singleton;
        _localPlayer = NetworkClient.connection.identity.GetComponent<PongPlayer>();
    }

    private void OnEnable()
    {
        InitializeHUDElements();
        PongPlayer.OnUpdateOpponentPlayerReady += UpdateOpponentStatus;
        PongPlayer.OnStartingGame += OnStartingGame;
        PongPlayer.OnGameOver += OnGameOver;
        ScoreManager.OnScoreUpdated += UpdateScore;
    }
    private void OnDisable()
    {
        PongPlayer.OnUpdateOpponentPlayerReady -= UpdateOpponentStatus;
        PongPlayer.OnStartingGame -= OnStartingGame;
        PongPlayer.OnGameOver -= OnGameOver;
        ScoreManager.OnScoreUpdated -= UpdateScore;
    }


    private void ReadyButtonClick()
    {
        if (NetworkServer.active)
            _leftStatus.text = "Ready";
        else _rightStatus.text = "Ready";

        _readyButton.gameObject.SetActive(false);
        //Notify the server that this local player is ready to start with a Command 
        _localPlayer.CmdSetPlayerReady(_localPlayer);
    }

    private void NewGameButtonClick()
    {
        _newGameButton.gameObject.SetActive(false);
        _localPlayer.CmdSetPlayerReadyForNewGame();
    }

    private void UpdateOpponentStatus()
    {
        if (NetworkServer.active)
            _rightStatus.text = "Ready";
        else _leftStatus.text = "Ready";
    }

    private void OnStartingGame()
    {
        _leftStatus.gameObject.SetActive(false);
        _rightStatus.gameObject.SetActive(false);

        _leftScore.gameObject.SetActive(true);
        _rightScore.gameObject.SetActive(true);

        StartCoroutine(Countdown(3f));
    }

    private void InitializeHUDElements()
    {
        _leftScore.gameObject.SetActive(false);
        _leftScore.text = "0";
        _rightScore.gameObject.SetActive(false);
        _rightScore.text = "0";

        _leftStatus.gameObject.SetActive(true);
        _leftStatus.text = "Not ready";

        _rightStatus.gameObject.SetActive(true);
        _rightStatus.text = "Not ready";

        _countdown.gameObject.SetActive(false);
        _countdown.text = "0";

        _gameOver.gameObject.SetActive(false);

        _newGameButton.gameObject.SetActive(false);
        _newGameButton.onClick.AddListener(NewGameButtonClick);

        _readyButton.gameObject.SetActive(true);
        _readyButton.onClick.AddListener(ReadyButtonClick);
    }

    private IEnumerator Countdown(float duration)
    {
        float startTime = Time.time;
        float elapsedTime = 0f;
        _countdown.gameObject.SetActive(true);
        while (elapsedTime < duration)
        {
            elapsedTime = Time.time - startTime;
            _countdown.text = ((int)duration - (int)elapsedTime).ToString();
            yield return null;
        }
        _countdown.gameObject.SetActive(false);
        _networkManager.SpawnBall();
    }

    private void UpdateScore(GameFieldSide gameFieldSide)
    {
        if (gameFieldSide == GameFieldSide.left)
        {
            int score = int.Parse(_rightScore.text);
            score++;
            _rightScore.text = score.ToString();
        }
        else
        {
            int score = int.Parse(_leftScore.text);
            score++;
            _leftScore.text = score.ToString();
        }
    }

    private void OnGameOver()
    {
        if (int.Parse(_rightScore.text) > int.Parse(_leftScore.text))
        {
            if (_localPlayer.isClientOnly) _gameOver.text = "You Win";
            else _gameOver.text = "You Lose";
        }
        else if (_localPlayer.isClientOnly) _gameOver.text = "You Lose";
        else _gameOver.text = "You Win";

        _gameOver.gameObject.SetActive(true);
        _newGameButton.gameObject.SetActive(true);
    }

}
