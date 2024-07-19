using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Mirror;
using System;

[RequireComponent(typeof(NetworkIdentity), typeof(Rigidbody2D))]
public class Ball : NetworkBehaviour
{
    [SerializeField] private float _speed = 30;
    private float _speedMultiplier = 1f;

    private Rigidbody2D _rigidbody;
    private PongNetworkManager _networkManager;
    private NetworkTransformUnreliable _networkTransform;
    private PongPlayer _localPlayer;

    private void Awake()
    {
        _rigidbody = GetComponent<Rigidbody2D>();
        _rigidbody.simulated = true;
        _speedMultiplier = 1f;
        _networkManager = (PongNetworkManager)NetworkManager.singleton;
        _localPlayer = NetworkClient.localPlayer.GetComponent<PongPlayer>();
        _networkTransform = GetComponent<NetworkTransformUnreliable>();
    }

    public override void OnStartServer()
    {
        _rigidbody.simulated = true;

        _rigidbody.velocity = GetStartRandomDirection() * _speed;
    }

    float HitFactor(Vector2 ballPos, Vector2 racketPos, float racketHeight)
    {
        return (ballPos.y - racketPos.y) / racketHeight;
    }


    [Server]
    void OnCollisionEnter2D(Collision2D col)
    {
        //Debug.Log("OnCollisionEnter2D");
        if (col.transform.TryGetComponent(out PlayerMovement playerRacket))
        {
            //Debug.Log("Player Hitted");
            float y = HitFactor(transform.position,
                                col.transform.position,
                                col.collider.bounds.size.y);

            float x = col.relativeVelocity.x > 0 ? 1 : -1;

            Vector2 dir = new Vector2(x, y).normalized;
            //Debug.Log("new direction: " + dir);
            _rigidbody.velocity = dir * _speed * _speedMultiplier;
            _speedMultiplier += _speedMultiplier * 0.05f;
            //Debug.Log("new speed: " + _speed);
        }
        if (col.gameObject.layer == 6) 
        {
            //Debug.Log("GOAL");
            GameFieldSide gameFieldSide;
            if (col.transform.position.x > 0) gameFieldSide = GameFieldSide.right;
            else gameFieldSide = GameFieldSide.left;
            _localPlayer.RpcScorePoint(gameFieldSide);
            ResetBall();
        }
    }

    private void ResetBall()
    {
        transform.position *= new Vector2(0f, 1f);
        _speedMultiplier = 1f;
        //_rigidbody.velocity = GetStartRandomDirection() * _speed;
        _rigidbody.velocity = Vector2.Reflect(_rigidbody.velocity, Vector2.up).normalized * _speed;
    }

    private Vector2 GetStartRandomDirection()
    {
        if (UnityEngine.Random.value > 0.5f)
            return Vector2.right;
        else return Vector2.left;
    }
}

public enum GameFieldSide
{
    left,
    right
}
