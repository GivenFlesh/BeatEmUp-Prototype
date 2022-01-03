using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

[RequireComponent(typeof(Mover),typeof(Rigidbody2D))]
public class Player : MonoBehaviour
{
    Vector2 rawInput;
    Rigidbody2D _rigidBody;
    Mover _mover;
    Jumper _jumper;
    [SerializeField] float moveSpeed = 200;

    void Awake()
    {
        _rigidBody = GetComponent<Rigidbody2D>();
        _mover = GetComponent<Mover>();
        _jumper = GetComponentInChildren<Jumper>();
    }

    void Start()
    {
        
    }

    void FixedUpdate()
    {
        _mover.Move(_rigidBody,moveSpeed,rawInput);
    }

    void OnMove(InputValue value)
    {
        rawInput = value.Get<Vector2>();
    }

    void OnJump(InputValue value)
    {
        _jumper.Jump();
    }
}
