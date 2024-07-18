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
    [SerializeField] private TextMeshProUGUI _score;
    [SerializeField] private TextMeshProUGUI _opponentScore;
    [SerializeField] private TextMeshProUGUI _status;
    [SerializeField] private TextMeshProUGUI _opponentStatus;
    [SerializeField] private Button _readyButton;

    private PongNetworkManager _networkManager;
    private GameManager _gameManager;

    private void Awake()
    {
        _networkManager = (PongNetworkManager)NetworkManager.singleton;
        _gameManager = GameManager.Singleton;
    }

    private void OnEnable()
    {
        _score.gameObject.SetActive(false);
        _score.text = "0";
        _opponentScore.gameObject.SetActive(false);
        _opponentScore.text = "0";

        _status.gameObject.SetActive(true);
        _status.text = "Not ready";

        _opponentStatus.gameObject.SetActive(true);
        _opponentStatus.text = "Not ready";

        _readyButton.gameObject.SetActive(true);
        _readyButton.onClick.AddListener(ReadyButtonClick);

        PongPlayer.OnUpdateOpponentPlayerReady += UpdateOpponentStatus;
        PongPlayer.OnStartingGame += StartingGame;

        StartCoroutine(SetHUDElementPositions());
    }
    private void OnDisable()
    {
        _readyButton.onClick.RemoveListener(ReadyButtonClick);
        PongPlayer.OnUpdateOpponentPlayerReady -= UpdateOpponentStatus;
        PongPlayer.OnStartingGame -= StartingGame;
    }


    private void ReadyButtonClick()
    {
        PongPlayer localPlayer = GetLocalPlayer();
        _status.text = "Ready";
        _readyButton.gameObject.SetActive(false);
        localPlayer.CmdSetPlayerReady(localPlayer);
    }

    private void UpdateOpponentStatus() => _opponentStatus.text = "Ready";

    private void StartingGame()
    {
        _status.gameObject.SetActive(false);
        _opponentStatus.gameObject.SetActive(false);

        _score.gameObject.SetActive(true);
        _opponentScore.gameObject.SetActive(true);

    }

    private PongPlayer GetLocalPlayer() => NetworkClient.connection.identity.GetComponent<PongPlayer>();


    private IEnumerator SetHUDElementPositions()
    {
        yield return new WaitForSeconds(1f);

        if (GetLocalPlayer().isClientOnly)
        {
            Debug.LogWarning("CLIENT");
            _status.rectTransform.rect.Set(300f, 0f, 200f, 50f);
            _score.rectTransform.rect.Set(300f, 470f, 200f, 50f);
            _opponentStatus.rectTransform.rect.Set(-300f, 0f, 200f, 50f);
            _opponentScore.rectTransform.rect.Set(-300f, 470f, 200f, 50f);
        }
        else
        {
            Debug.LogWarning("HOST");
            _status.rectTransform.rect.Set(-300f, 0f, 200f, 50f);
            _score.rectTransform.rect.Set(-300f, 470f, 200f, 50f);
            _opponentStatus.rectTransform.rect.Set(300f, 0f, 200f, 50f);
            _opponentScore.rectTransform.rect.Set(300f, 470f, 200f, 50f);
        }
    }

}
