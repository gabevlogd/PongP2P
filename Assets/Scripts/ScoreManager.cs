using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Mirror;
using System;

public class ScoreManager : MonoBehaviour
{
    // left is HOST
    // right is CLIENT
    private int _leftScore;
    private int _rightScore;

    private PongPlayer _localPlayer;
    public static event Action<GameFieldSide> OnScoreUpdated;

    private void Awake()
    {
        _localPlayer = NetworkClient.localPlayer.GetComponent<PongPlayer>();
    }

    private void OnEnable()
    {
        PongPlayer.OnPointScored += UpdateScore;
    }

    private void OnDisable()
    {
        PongPlayer.OnPointScored -= UpdateScore;
    }

    private void UpdateScore(GameFieldSide gameFieldSide)
    {
        if (gameFieldSide == GameFieldSide.left)
        {
            _rightScore++;
        }
        else
        {
            _leftScore++;
        }
        OnScoreUpdated?.Invoke(gameFieldSide);
    }
}
