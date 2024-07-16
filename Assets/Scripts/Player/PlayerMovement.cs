using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Mirror;
using UnityEngine.InputSystem;

[RequireComponent(typeof(Rigidbody2D))]
public class PlayerMovement : NetworkBehaviour
{
    [SerializeField] private float _speed = 500f;
    private Rigidbody2D _rigidbody;
    private PlayerInput _input;

    private void Awake()
    {
        _rigidbody = GetComponent<Rigidbody2D>();
        _input = InputManager.Input;
    }

    private void OnEnable()
    {
        _input.Movement.Move.started += OnStartMove;
        _input.Movement.Move.canceled += OnCancelMove;
    }

    private void OnDisable()
    {
        _input.Movement.Move.started -= OnStartMove;
        _input.Movement.Move.canceled -= OnCancelMove;
    }

    private void OnStartMove(InputAction.CallbackContext callbackContext)
    {
        if (!isLocalPlayer) return;
        Vector2 moveVec = _input.Movement.Move.ReadValue<Vector2>() * _speed * Time.fixedDeltaTime;
        _rigidbody.velocity = moveVec;
    }

    private void OnCancelMove(InputAction.CallbackContext callbackContext)
    {
        if (!isLocalPlayer) return;
        _rigidbody.velocity = Vector2.zero;
    }
}
